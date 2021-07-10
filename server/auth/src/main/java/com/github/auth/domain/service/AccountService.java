package com.github.auth.domain.service;

import com.github.auth.domain.account.dto.AccountRequest;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.LoginRequest;

public interface AccountService {
    AuthResponse login(LoginRequest request);
    AuthResponse register(AccountRequest request);
    AuthResponse getAccessToken(String refreshToken);
    void revokeToken(String userid);
}
