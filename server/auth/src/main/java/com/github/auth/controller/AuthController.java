package com.github.auth.controller;

import com.github.auth.domain.dto.AuthRequest;
import com.github.auth.domain.service.AuthService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class AuthController {
    private final AuthService authService;

    @PostMapping("/signup")
    public ResponseEntity<?> signup(@RequestBody AuthRequest request) {
        return authService.register(request);
    }

    @PostMapping("/signin")
    public ResponseEntity<?> signin(@RequestBody AuthRequest request) {
        return authService.login(request);
    }

    @DeleteMapping("/signout/{userid}")
    public void signout(@PathVariable String userid) {
        authService.revokeToken(userid);
    }
}
