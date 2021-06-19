package com.github.auth.domain.service;

import com.github.auth.domain.dto.AuthRequest;
import org.springframework.http.ResponseEntity;

public interface AuthService {
    ResponseEntity<?> login(AuthRequest request);
    ResponseEntity<?> register(AuthRequest request);
    void revokeToken(String userid);
}
