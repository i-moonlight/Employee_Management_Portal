package com.github.auth.domain.service;

import com.github.auth.domain.password.model.ResetPasswordRequest;
import org.springframework.http.ResponseEntity;

public interface PasswordService {
    String sendResetPasswordToken(String email);
    ResponseEntity<?> changePasswordByEmail(ResetPasswordRequest request);
}
