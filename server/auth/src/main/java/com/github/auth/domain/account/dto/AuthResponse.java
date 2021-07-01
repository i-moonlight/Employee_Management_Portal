package com.github.auth.domain.account.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;

@AllArgsConstructor
@Schema(description = "Response for create account")
@Builder
@Data
public class AuthResponse {
    @Schema(description = "code status", example = "200")
    private int status;

    @Schema(description = "message", example = "Registration successful")
    private String message;

    @Schema(description = "access token", example = "token")
    private String accessToken;

    @Schema(description = "refresh token", example = "token")
    private String refreshToken;
    
    private UserInfoData userInfoData;

    public AuthResponse(int status, String message) {
        this.status = status;
        this.message = message;
    }
}
