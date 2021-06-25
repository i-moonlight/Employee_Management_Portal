package com.github.auth.domain.password.dto;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@Data
@NoArgsConstructor
public class ResetPasswordRequest {
    private String accessToken;
    private String email;
    private String newPassword;
}