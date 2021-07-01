package com.github.auth.domain.service;

import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.password.dto.ResetPasswordRequest;
import org.springframework.http.ResponseEntity;

public interface PasswordService {
    String sendResetPasswordToken(String email);
    AuthResponse changePasswordByEmail(ResetPasswordRequest request);
}
