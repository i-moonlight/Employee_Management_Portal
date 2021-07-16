package com.github.auth.domain.object.jwt.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class JwtRequest {
    @Schema(description = "refreshToken", example = "eyJhbGciOiJIUzI1NiJ9.eyJyb2xlIjpbeyJhdXRob3Jpd")
    @NotBlank(message = "Refresh token cannot be blank")
    private String refreshToken;
}
