package com.github.auth.domain.service.implementation;

import com.github.auth.domain.dto.AuthRequest;
import com.github.auth.domain.dto.AuthResponse;
import com.github.auth.domain.dto.UserInfoData;
import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.Role;
import com.github.auth.domain.model.User;
import com.github.auth.domain.repository.AuthRepository;
import com.github.auth.domain.service.AuthService;
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
public class DaoAuthService implements AuthService {
    private final AuthRepository repository;
    private final JwtService jwtService;
    private final PasswordEncoder passwordEncoder;

    public ResponseEntity<?> registration(@NotNull AuthRequest request) {
        Optional<User> authUser = repository.getUserByName(request.getUsername());

        if (authUser.isPresent()) {
            return new ResponseEntity<>("User already exists", HttpStatus.CONFLICT);
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

        repository.saveUser(user);

        UserDetails userDetails = new AuthUserDetails(user);
        String accessToken = jwtService.generateToken(userDetails);
        String refreshToken = jwtService.generateRefreshToken(userDetails);

        UserInfoData userInfo = UserInfoData.builder()
                .id(uuid)
                .firstname(user.getFirstname())
                .lastname(user.getLastname())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();
        return ResponseEntity.ok(AuthResponse.builder()
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoData(userInfo)
                .build());
    }
}
