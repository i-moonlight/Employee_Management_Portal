package com.github.auth.domain.service;

import com.github.auth.domain.account.dto.AuthRequest;
import org.springframework.http.ResponseEntity;

public interface AccountService {
    ResponseEntity<?> login(AuthRequest request);
    ResponseEntity<?> register(AuthRequest request);
    void revokeToken(String userid);
}
