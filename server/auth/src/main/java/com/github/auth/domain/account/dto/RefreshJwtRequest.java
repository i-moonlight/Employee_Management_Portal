package com.github.auth.domain.account.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class RefreshJwtRequest {
    @Schema(description = "refreshToken",
            example = "eyJhbGciOiJIUzI1NiJ9.eyJyb2xlIjpbeyJhdXRob3Jpd")
    public String refreshToken;
}
