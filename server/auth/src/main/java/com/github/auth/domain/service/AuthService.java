package com.github.auth.domain.service;

import com.github.auth.domain.dto.AuthRequest;
import org.springframework.http.ResponseEntity;

public interface AuthService {
    ResponseEntity<?> register(AuthRequest request);
    ResponseEntity<?> login(AuthRequest request);
}
