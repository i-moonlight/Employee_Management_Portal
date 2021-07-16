package com.github.auth.domain.object.jwt.service;

import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.User;
import com.github.auth.domain.object.account.dto.AuthResponse;
import com.github.auth.domain.object.jwt.props.JwtProperties;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.TokenService;
import io.jsonwebtoken.*;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import jakarta.security.auth.message.AuthException;
import lombok.NonNull;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import javax.crypto.SecretKey;
import java.security.Key;
import java.time.Instant;
import java.time.temporal.ChronoUnit;
import java.util.*;
import java.util.function.Function;

import static org.springframework.http.HttpStatus.*;

@Slf4j
@Service
@RequiredArgsConstructor
public class JwtTokenService implements TokenService {
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;
    private final JwtProperties jwtExpiration;

    public boolean validateRefreshToken(@NonNull String refreshToken) {
        return validateToken(refreshToken, getSignInKey());
    }

    private boolean validateToken(@NonNull String refreshToken, @NonNull Key secret) {
        try {
            Jwts.parserBuilder().setSigningKey(secret).build().parseClaimsJws(refreshToken);
            return true;
        } catch (ExpiredJwtException eje) {
            log.error("Token expired", eje);
        } catch (UnsupportedJwtException uje) {
            log.error("Unsupported jwt", uje);
        } catch (MalformedJwtException me) {
            log.error("Malformed jwt", me);
        } catch (SecurityException se) {
            log.error("Invalid signature", se);
        } catch (Exception e) {
            log.error("invalid token", e);
        }
        return false;
    }

    public Claims getRefreshClaims(@NonNull String refreshToken) {
        return getClaims(refreshToken, getSignInKey());
    }

    private Claims getClaims(@NonNull String refreshToken, @NonNull Key secret) {
        return Jwts.parserBuilder().setSigningKey(secret).build().parseClaimsJws(refreshToken).getBody();
    }

    public String generateToken(UserDetails userDetails) {
        return buildToken(new HashMap<>(), userDetails, jwtExpiration.getExpireAccessToken());
    }

    public String buildToken(Map<String, Object> extraClaims, @NonNull UserDetails userDetails, long expiration) {
        Instant validity = Instant.now()
                .plus(expiration, ChronoUnit.MINUTES);
        return Jwts.builder()
                .setClaims(extraClaims)
                .claim("role", userDetails.getAuthorities())
                .setSubject(userDetails.getUsername())
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(Date.from(validity))
                .signWith(getSignInKey(), SignatureAlgorithm.HS256)
                .compact();
    }

    public boolean isTokenValid(String token, @NonNull UserDetails userDetails) {
        final String username = extractUsername(token);
        return (username.equals(userDetails.getUsername())) && !isTokenExpired(token);
    }

    public String extractUsername(String token) {
        return extractClaim(token, Claims::getSubject);
    }

    public <T> T extractClaim(String token, @NonNull Function<Claims, T> claimsResolver) {
        final Claims claims = extractAllClaims(token);
        return claimsResolver.apply(claims);
    }

    private boolean isTokenExpired(String token) {
        return extractExpiration(token).before(new Date());
    }

    private Date extractExpiration(String token) {
        return extractClaim(token, Claims::getExpiration);
    }

    private Claims extractAllClaims(String token) {
        return Jwts.parserBuilder().setSigningKey(getSignInKey()).build().parseClaimsJws(token).getBody();
    }

    private @NonNull SecretKey getSignInKey() {
        byte[] keyBytes = Decoders.BASE64.decode(jwtExpiration.getSecret());
        return Keys.hmacShaKeyFor(keyBytes);
    }

    @SneakyThrows
    public AuthResponse getAccessToken(String refreshToken) {
        if (validateRefreshToken(refreshToken)) {
            Claims claims = getRefreshClaims(refreshToken);
            String login = claims.getSubject();
            String saveRefreshToken = tokenRepository.getToken(login);

            if (saveRefreshToken != null && saveRefreshToken.equals(refreshToken)) {
                User authUser = userRepository.findUserByName(login)
                    .orElseThrow(() -> new AuthException("User not found"));
                UserDetails userDetails = new AuthUserDetails(authUser);
                String newAccessToken = generateToken(userDetails);

                return AuthResponse.builder()
                    .status(OK)
                    .message("accessToken")
                    .accessToken(newAccessToken).build();
            }
        }

        return new AuthResponse(BAD_REQUEST, "Refresh token is not valid");
    }

    @SneakyThrows
    public AuthResponse getRefreshToken(@NonNull String refreshToken) {
        if (validateRefreshToken(refreshToken)) {
            Claims claims = getRefreshClaims(refreshToken);
            String username = claims.getSubject();
            String saveRefreshToken = tokenRepository.getToken(username);

            if (saveRefreshToken != null && saveRefreshToken.equals(refreshToken)) {
                User authUser = userRepository.findUserByName(username)
                    .orElseThrow(() -> new AuthException("User is not found"));
                UserDetails userDetails = new AuthUserDetails(authUser);
                String newAccessToken = generateToken(userDetails);
                final String newRefreshToken = generateToken(userDetails);
                tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

                return AuthResponse.builder()
                    .status(OK)
                    .message("get new tokens")
                    .accessToken(newAccessToken)
                    .refreshToken(newRefreshToken).build();
            }
        }
        return new AuthResponse(FORBIDDEN, "Invalid JWT token. You need to register!");
    }

    @Override
    public void deleteRefreshToken(String userid) {
        Optional<User> authUser = userRepository.findUserById(UUID.fromString(userid));
        if (authUser.isPresent()) {
            UserDetails userDetails = new AuthUserDetails(authUser.get());
            tokenRepository.deleteToken(userDetails.getUsername());
        }
    }
}
