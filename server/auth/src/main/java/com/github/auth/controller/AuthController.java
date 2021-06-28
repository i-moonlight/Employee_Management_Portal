package com.github.auth.controller;

import com.github.auth.domain.account.dto.AuthRequest;
import com.github.auth.domain.password.dto.ResetPasswordRequest;
import com.github.auth.domain.service.AccountService;
import com.github.auth.domain.service.PasswordService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
@Validated
public class AuthController {
    private final AccountService accountService;
    private final PasswordService passwordService;

    @PostMapping("/signup")
    public ResponseEntity<?> signup(@Validated @RequestBody AuthRequest request) {
        return accountService.register(request);
    }

    @PostMapping("/signin")
    public ResponseEntity<?> signin(@Validated @RequestBody AuthRequest request) {
        return accountService.login(request);
    }

    @DeleteMapping("/signout/{userid}")
    public void signout(@PathVariable("userid") @Validated String userid) {
        accountService.revokeToken(userid);
    }

    @PostMapping("/forgot-password")
    public String processForgotPassword(@Validated String email) {
        return passwordService.sendResetPasswordToken(email);
    }

    @PostMapping("/change-password")
    public ResponseEntity<?> changePassword(@Validated ResetPasswordRequest request) {
        return passwordService.changePasswordByEmail(request);
    }
}
