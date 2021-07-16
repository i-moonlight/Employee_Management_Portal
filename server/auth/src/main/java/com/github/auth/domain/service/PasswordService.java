package com.github.auth.domain.service;

import com.github.auth.domain.object.account.dto.AuthResponse;
import com.github.auth.domain.object.password.dto.EmailMessage;
import com.github.auth.domain.object.password.dto.PasswordResetRequest;

public interface PasswordService {
    AuthResponse sendResetPasswordLink(EmailMessage email);
    AuthResponse changePasswordByToken(PasswordResetRequest request, String resetToken);
}
