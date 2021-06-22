package com.github.auth.controller;

import com.github.auth.domain.dto.AuthRequest;
import com.github.auth.domain.password.model.ResetPasswordRequest;
import com.github.auth.domain.service.AuthService;
import com.github.auth.domain.service.PasswordService;
import lombok.RequiredArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
public class AuthController {
    private final AuthService authService;
    private final PasswordService passwordService;

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

    @PostMapping("/forgot-password")
    public String processForgotPassword(String email) {
        return passwordService.sendResetPasswordToken(email);
    }

    @PostMapping("/change-password")
    public ResponseEntity<?> changePassword(@NotNull ResetPasswordRequest request) {
        return passwordService.changePasswordByEmail(request);
    }
}
