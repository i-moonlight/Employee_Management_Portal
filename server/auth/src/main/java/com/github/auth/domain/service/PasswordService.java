package com.github.auth.domain.service;

import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.PasswordResetRequest;

public interface PasswordService {
    AuthResponse sendResetPasswordLink(EmailMessage email);
    AuthResponse changePasswordByToken(PasswordResetRequest request, String resetToken);
}
