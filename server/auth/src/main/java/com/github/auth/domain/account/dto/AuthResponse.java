package com.github.auth.domain.account.dto;

import lombok.Builder;
import lombok.Data;

@Builder
@Data
public class AuthResponse {
    private String accessToken;
    private String refreshToken;
    private UserInfoData userInfoData;
}
