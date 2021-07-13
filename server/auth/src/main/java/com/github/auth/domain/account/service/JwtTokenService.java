package com.github.auth.domain.account.service;

import com.github.auth.domain.account.props.JwtProperties;
import io.jsonwebtoken.*;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import lombok.NonNull;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import javax.crypto.SecretKey;
import java.security.Key;
import java.time.Instant;
import java.time.temporal.ChronoUnit;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

@Slf4j
@Service
//@RequiredArgsConstructor
public class JwtTokenService {
    private final JwtProperties jwtExpiration;
    private final SecretKey jwtSecret;

    public JwtTokenService(JwtProperties jwtExpiration) {
        this.jwtExpiration = new JwtProperties();
        this.jwtSecret = Keys.hmacShaKeyFor(Decoders.BASE64.decode(jwtExpiration.getSecret()));
    }

    public boolean validateRefreshToken(@NonNull String refreshToken) {
        return validateToken(refreshToken, jwtSecret);
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
        return getClaims(refreshToken, jwtSecret);
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

    private @NonNull Key getSignInKey() {
        byte[] keyBytes = Decoders.BASE64.decode(jwtExpiration.getSecret());
        return Keys.hmacShaKeyFor(keyBytes);
    }
}
