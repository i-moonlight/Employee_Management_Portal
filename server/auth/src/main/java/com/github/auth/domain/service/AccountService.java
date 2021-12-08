package com.github.auth.domain.service;

import com.github.auth.domain.object.account.dto.AccountRequest;
import com.github.auth.domain.object.account.dto.AuthResponse;
import com.github.auth.domain.object.account.dto.LoginRequest;
import org.springframework.stereotype.Component;

@Component
public interface AccountService {
    AuthResponse login(LoginRequest request);
    AuthResponse register(AccountRequest request);
}
