package com.github.auth.domain.account.service;

import com.github.auth.domain.account.dto.AccountRequest;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.LoginRequest;
import com.github.auth.domain.account.dto.UserInfoObject;
import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.Role;
import com.github.auth.domain.model.User;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.AccountService;
import io.jsonwebtoken.Claims;
import jakarta.security.auth.message.AuthException;
import lombok.NonNull;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.UUID;

import static org.springframework.http.HttpStatus.*;

@Service
@RequiredArgsConstructor
public class RepositoryAccountService implements AccountService {
    private final PasswordEncoder passwordEncoder;
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;
    private final JwtTokenService jwtTokenService;

    public AuthResponse login(@NonNull LoginRequest request) {
        // Auth user check by username
        Optional<User> authUser = userRepository.findUserByName(request.getUsername());
        if (authUser.isEmpty())
            return new AuthResponse(BAD_REQUEST.value(), "User " + request.getUsername() + " doesn't exist");

        // Auth password check
        boolean passChecker = passwordEncoder.matches(request.getPassword(), authUser.get().getPassword());
        if (!passChecker) {
            return new AuthResponse(BAD_REQUEST.value(), "Incorrect password");
        }

        UserDetails userDetails = new AuthUserDetails(authUser.get());
        String accessToken = jwtTokenService.generateToken(userDetails);
        String refreshToken = jwtTokenService.generateToken(userDetails);
        tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

        UserInfoObject userInfo = UserInfoObject.builder()
                .id(authUser.get().getId())
                .firstname(authUser.get().getFirstname())
                .lastname(authUser.get().getLastname())
                .username(authUser.get().getUsername())
                .email(authUser.get().getEmail())
                .build();

        return AuthResponse.builder()
                .status(OK.value())
                .message("Login successful")
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoObject(userInfo)
                .build();
    }

    public AuthResponse register(@NonNull AccountRequest request) {
        // Auth user check by username
        Optional<User> authUser = userRepository.findUserByName(request.getUsername());
        if (authUser.isPresent()) {
            return new AuthResponse(BAD_REQUEST.value(), "User " + request.getUsername() + " already exists");
        }

        // Auth user password check by compare
        if (!request.getPassword().equals(request.getPasswordConfirm())) {
            return new AuthResponse(BAD_REQUEST.value(), "Password and password confirmation do not match");
        }

        UUID uuid = UUID.randomUUID();
        User user = User.builder()
                .id(uuid)
                .firstname(request.getFirstname())
                .lastname(request.getLastname())
                .username(request.getUsername())
                .email(request.getEmail())
                .password(passwordEncoder.encode(request.getPassword()))
                .role(Role.USER)
                .build();
        userRepository.saveUser(user);

        UserDetails userDetails = new AuthUserDetails(user);
        String accessToken = jwtTokenService.generateToken(userDetails);
        String refreshToken = jwtTokenService.generateToken(userDetails);
        tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

        UserInfoObject userInfo = UserInfoObject.builder()
                .id(uuid)
                .firstname(user.getFirstname())
                .lastname(user.getLastname())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();

        return AuthResponse.builder()
                .status(OK.value())
                .message("Registration successful")
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoObject(userInfo)
                .build();
    }

    @SneakyThrows
    public AuthResponse getAccessToken(String refreshToken) {
        if (jwtTokenService.validateRefreshToken(refreshToken)) {
            Claims claims = jwtTokenService.getRefreshClaims(refreshToken);
            String login = claims.getSubject();
            String saveRefreshToken = tokenRepository.getToken(login);

            if (saveRefreshToken != null && saveRefreshToken.equals(refreshToken)) {
                User authUser = userRepository.findUserByName(login)
                        .orElseThrow(() -> new AuthException("User not found"));
                UserDetails userDetails = new AuthUserDetails(authUser);
                String newAccessToken = jwtTokenService.generateToken(userDetails);

                return AuthResponse.builder().status(OK.value())
                        .message("accessToken")
                        .accessToken(newAccessToken).build();
            }
        }

        return new AuthResponse(BAD_REQUEST.value(), "Refresh token is not valid");
    }


    @SneakyThrows
    public AuthResponse getRefreshToken(@NonNull String refreshToken) {
        if (jwtTokenService.validateRefreshToken(refreshToken)) {
            Claims claims = jwtTokenService.getRefreshClaims(refreshToken);
            String username = claims.getSubject();
            String saveRefreshToken = tokenRepository.getToken(username);

            if (saveRefreshToken != null && saveRefreshToken.equals(refreshToken)) {
                User authUser = userRepository.findUserByName(username)
                        .orElseThrow(() -> new AuthException("User is not found"));
                UserDetails userDetails = new AuthUserDetails(authUser);
                String newAccessToken = jwtTokenService.generateToken(userDetails);
                final String newRefreshToken = jwtTokenService.generateToken(userDetails);
                tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

                return AuthResponse.builder().status(OK.value())
                        .message("get new tokens")
                        .accessToken(newAccessToken)
                        .refreshToken(newRefreshToken)
                        .build();
            }
        }
        return new AuthResponse(FORBIDDEN.value(), "Invalid JWT token. You need to register!");
    }

    @Override
    public void revokeToken(String userid) {
        Optional<User> authUser = userRepository.findUserById(UUID.fromString(userid));
        if (authUser.isPresent()) {
            UserDetails userDetails = new AuthUserDetails(authUser.get());
            tokenRepository.deleteToken(userDetails.getUsername());
        }
    }
}
