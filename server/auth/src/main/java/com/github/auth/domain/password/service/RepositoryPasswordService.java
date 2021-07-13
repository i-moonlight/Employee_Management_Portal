package com.github.auth.domain.password.service;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.UserInfoObject;
import com.github.auth.domain.account.service.JwtTokenService;
import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.User;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.PasswordResetObject;
import com.github.auth.domain.password.dto.PasswordResetRequest;
import com.github.auth.domain.repository.TokenRepository;
import com.github.auth.domain.repository.UserRepository;
import com.github.auth.domain.service.EmailService;
import com.github.auth.domain.service.PasswordService;
import lombok.NonNull;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Calendar;
import java.util.Optional;
import java.util.UUID;

import static org.springframework.http.HttpStatus.BAD_REQUEST;
import static org.springframework.http.HttpStatus.OK;

@Service
@RequiredArgsConstructor
public class RepositoryPasswordService implements PasswordService {
    private final EmailService emailSenderService;
    private final PasswordEncoder passwordEncoder;
    private final TokenRepository<String, String> tokenRepository;
    private final UserRepository userRepository;
    private final JwtTokenService jwtTokenService;
    private final ObjectMapper objectMapper;

    @Override
    public AuthResponse sendResetPasswordLink(@NonNull EmailMessage email) {
        Optional<User> user = userRepository.findUserByEmail(email.getTo());
        if (user.isEmpty()) {
            return new AuthResponse(BAD_REQUEST.value(), "User with email " + email + " not found");
        }
        sendEmailToken(user.get(), "change-password");
        return new AuthResponse(OK.value(), "Please check your email for password reset");
    }

    public void sendEmailToken(@NonNull User user, String process) {
        PasswordResetObject object = new PasswordResetObject();
        object.setToken(UUID.randomUUID().toString());
        object.setUser(user);

        try {
            String json = objectMapper.writeValueAsString(object);
            tokenRepository.saveToken(object.getToken(), json);
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }

        String link = "http://localhost:4200/auth/" + process + "?token=" + object.getToken();
        String content = "To reset your password, click the link: " + link;
        String subject = "Reset password";
        emailSenderService.sendEmail(user.getEmail(), content, subject);
    }

    @Override
    public AuthResponse changePasswordByToken(@NonNull PasswordResetRequest request, String resetPasswordToken) {
        Object[] tokenVerificationResult = validatePasswordResetToken(resetPasswordToken);
        String result = tokenVerificationResult[0].toString();
        User user = (User) tokenVerificationResult[1];

        if (!result.equalsIgnoreCase("valid")) {
            return new AuthResponse(BAD_REQUEST.value(), "Invalid token password reset token");
        }

        if (!request.getNewPassword().equals(request.getPasswordConfirm())) {
            return new AuthResponse(BAD_REQUEST.value(), "Password and password confirmation do not match");
        }

        user.setPassword(passwordEncoder.encode(request.getNewPassword()));
        userRepository.updateUserPassword(user);

        UserDetails userDetails = new AuthUserDetails(user);
        String accessToken = jwtTokenService.generateToken(userDetails);
        String refreshToken = jwtTokenService.generateToken(userDetails);
        tokenRepository.saveToken(userDetails.getUsername(), refreshToken);
        tokenRepository.deleteToken(resetPasswordToken);

        UserInfoObject userInfo = UserInfoObject.builder()
                .id(user.getId())
                .firstname(user.getFirstname())
                .lastname(user.getLastname())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();

        return AuthResponse.builder()
                .status(OK.value())
                .message("Password changed successfully")
                .accessToken(accessToken)
                .refreshToken(refreshToken)
                .userInfoObject(userInfo)
                .build();
    }

    private Object[] validatePasswordResetToken(@NonNull String resetToken) {
        String json = tokenRepository.getToken(resetToken);
        PasswordResetObject object;
        try {
            object = objectMapper.readValue(json, PasswordResetObject.class);
            if (object == null) {
                return new Object[]{"Invalid verification token"};
            }
            Calendar calendar = Calendar.getInstance();
            if ((object.getExpiry().getTime() - calendar.getTime().getTime()) <= 0) {
                return new Object[]{"Link already expired, resend link"};
            }
        } catch (JsonProcessingException e) {
            throw new RuntimeException(e);
        }

        return new Object[]{"valid", object.getUser()};
    }
}
