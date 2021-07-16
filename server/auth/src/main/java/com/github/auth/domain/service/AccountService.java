package com.github.auth.domain.service;

import com.github.auth.domain.object.account.dto.AccountRequest;
import com.github.auth.domain.object.account.dto.AuthResponse;
import com.github.auth.domain.object.account.dto.LoginRequest;

public interface AccountService {
    AuthResponse login(LoginRequest request);
    AuthResponse register(AccountRequest request);
}
