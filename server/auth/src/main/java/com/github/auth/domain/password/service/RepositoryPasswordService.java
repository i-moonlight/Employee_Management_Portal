package com.github.auth.domain.password.service;

import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.UserInfoData;
import com.github.auth.domain.account.service.JwtTokenService;
import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.User;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.PasswordResetToken;
import com.github.auth.domain.password.dto.ResetPasswordRequest;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.EmailService;
import com.github.auth.domain.service.PasswordService;
import lombok.NonNull;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RepositoryPasswordService implements PasswordService {
    private final EmailService emailSenderService;
    private final PasswordEncoder passwordEncoder;
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;
    private final JwtTokenService jwtTokenService;

    @Override
    public AuthResponse sendResetPasswordToken(@NonNull EmailMessage email) {
        Optional<User> user = userRepository.findUserByEmail(email.getTo());
        if (user.isEmpty()) {
            return new AuthResponse(HttpStatus.BAD_REQUEST.value(), "User with email " + email + " not found");
        }
        sendEmailToken(user.get(), "reset-password");
        return new AuthResponse(HttpStatus.OK.value(), "Please check your email for password reset");
    }

    public void sendEmailToken(@NonNull User user, String process) {
        PasswordResetToken token = new PasswordResetToken();
        token.setToken(UUID.randomUUID().toString());
        token.setUser(user);
        token.setExpiryDate(30);
        tokenRepository.saveToken(token.getUser().getUsername(), String.valueOf(token));

        String link = "http://localhost:4200/auth/" + process + "?token=" + token.getToken();
        String content = "To reset your password, click the link: " + link;
        String subject = "Reset password";
        emailSenderService.sendEmail(user.getEmail(), content, subject);
    }

    @Override
    public AuthResponse changePasswordByEmail(@NonNull ResetPasswordRequest request) {
        String token = tokenRepository.getToken(request.getEmail());

        if (token == null) {
            return new AuthResponse(HttpStatus.BAD_REQUEST.value(), "Invalid token");
        }

        Optional<User> authUser = userRepository.findUserByEmail(request.getEmail());
        String newPasswordEncode = passwordEncoder.encode(request.getNewPassword());

        if (authUser.isEmpty()) {
            return new AuthResponse(HttpStatus.BAD_REQUEST.value(), "Enter email");
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
