package com.github.auth.domain.service;

import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.ResetPasswordRequest;

public interface PasswordService {
    AuthResponse sendResetPasswordToken(EmailMessage email);
    AuthResponse changePasswordByEmail(ResetPasswordRequest request);
}
