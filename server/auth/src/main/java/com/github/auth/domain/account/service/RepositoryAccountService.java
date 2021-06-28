package com.github.auth.domain.account.service;

import com.github.auth.domain.account.dto.AuthRequest;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.UserInfoData;
import com.github.auth.domain.account.model.AuthUserDetails;
import com.github.auth.domain.account.model.Role;
import com.github.auth.domain.account.model.User;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.AccountService;
import lombok.RequiredArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RepositoryAccountService implements AccountService {
    private final JwtTokenService jwtTokenService;
    private final PasswordEncoder passwordEncoder;
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;

    public ResponseEntity<?> login(@NotNull AuthRequest request) {
        Optional<User> authUser = userRepository.findUserByName(request.getUsername());

        // Auth username check
        if (authUser.isEmpty()) {
            return new ResponseEntity<>("Пользователь " + request.getUsername() + " не существует", HttpStatus.BAD_REQUEST);
        }

        // Auth password check
        boolean passChecker = passwordEncoder.matches(request.getPassword(), authUser.get().getPassword());
        if (!passChecker) {
            return new ResponseEntity<>("Неверный пароль", HttpStatus.BAD_REQUEST);
        }

        UserDetails userDetails = new AuthUserDetails(authUser.get());
        String accessToken = jwtTokenService.generateToken(userDetails);
        String refreshToken = jwtTokenService.generateRefreshToken(userDetails);
        tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

        UserInfoData userInfo = UserInfoData.builder()
                .id(authUser.get().getId())
                .firstname(authUser.get().getFirstname())
                .lastname(authUser.get().getLastname())
                .username(authUser.get().getUsername())
                .email(authUser.get().getEmail())
                .build();

        return ResponseEntity.ok(AuthResponse.builder()
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoData(userInfo)
                .build());
    }

    public ResponseEntity<?> register(@NotNull AuthRequest request) {
        Optional<User> authUser = userRepository.findUserByName(request.getUsername());

        if (authUser.isPresent()) {
            return ResponseEntity.ok(AuthResponse.builder()
                    .status(HttpStatus.BAD_REQUEST.value())
                    .message("Пользователь " + request.getUsername() + " уже существует")
                    .build()
            );
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
        String refreshToken = jwtTokenService.generateRefreshToken(userDetails);
        tokenRepository.saveToken(userDetails.getUsername(), refreshToken);

        UserInfoData userInfo = UserInfoData.builder()
                .id(uuid)
                .firstname(user.getFirstname())
                .lastname(user.getLastname())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();

        return ResponseEntity.ok(AuthResponse.builder()
                .status(HttpStatus.OK.value())
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoData(userInfo)
                .build()
        );
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
