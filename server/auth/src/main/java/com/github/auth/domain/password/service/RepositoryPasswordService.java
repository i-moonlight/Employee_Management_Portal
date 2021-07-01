package com.github.auth.domain.password.service;

import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.UserInfoData;
import com.github.auth.domain.account.model.AuthUserDetails;
import com.github.auth.domain.account.model.User;
import com.github.auth.domain.account.service.JwtTokenService;
import com.github.auth.domain.password.dto.ResetPasswordRequest;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.PasswordService;
import lombok.RequiredArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RepositoryPasswordService implements PasswordService {
    private final EmailService emailService;
    private final JwtTokenService jwtTokenService;
    private final PasswordEncoder passwordEncoder;
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;

    @Override
    public String sendResetPasswordToken(String email) {
        Optional<User> user = userRepository.findUserByEmail(email);
        if (user.isEmpty()) return "Email invalid";
        sendEmailToken(user.get(), "reset-password");
        return "Please check your email inbox for password reset instructions.";
    }

    public void sendEmailToken(@NotNull User user, String process) {
        String token = UUID.randomUUID().toString();

        UserDetails userDetails = new AuthUserDetails(user);
        String accessToken = jwtTokenService.generateToken(userDetails);
        tokenRepository.saveToken(user.getEmail(), accessToken);

        String link = "http://localhost:4200/api/auth/" + process + "?token=" + token;
        emailService.sendSimpleEmail(user.getEmail(), "Click on link to change password", link);
    }

    @Override
    public AuthResponse changePasswordByEmail(@NotNull ResetPasswordRequest request) {
        String token = tokenRepository.getToken(request.getEmail());

        if (token == null) {
            return new AuthResponse(HttpStatus.BAD_REQUEST.value(),"Invalid token");
        }

        Optional<User> authUser = userRepository.findUserByEmail(request.getEmail());
        String newPasswordEncode = passwordEncoder.encode(request.getNewPassword());

        if (authUser.isEmpty()) {
            return new AuthResponse(HttpStatus.BAD_REQUEST.value(),"Enter email");
        }
        authUser.get().setPassword(newPasswordEncode);
        userRepository.saveUser(authUser.get());

        UserDetails userDetails = new AuthUserDetails(authUser.get());
        String accessToken = jwtTokenService.generateToken(userDetails);
        String refreshToken = jwtTokenService.generateRefreshToken(userDetails);
        tokenRepository.saveToken(userDetails.getPassword(), refreshToken);

        UserInfoData userInfo = UserInfoData.builder()
                .id(authUser.get().getId())
                .firstname(authUser.get().getFirstname())
                .lastname(authUser.get().getLastname())
                .username(authUser.get().getUsername())
                .email(authUser.get().getEmail())
                .build();

        return AuthResponse.builder()
                .status(HttpStatus.OK.value())
                .message("Password changed successful")
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoData(userInfo)
                .build();
    }
}
