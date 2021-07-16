package com.github.auth.domain.service;

import com.github.auth.domain.object.account.dto.AuthResponse;

public interface TokenService {
   AuthResponse getAccessToken(String refreshToken);
   AuthResponse getRefreshToken(String refreshToken);
   void deleteRefreshToken(String userid);
}
