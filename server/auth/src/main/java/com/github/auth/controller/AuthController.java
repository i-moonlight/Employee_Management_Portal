package com.github.auth.controller;

import com.github.auth.domain.account.dto.AccountRequest;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.LoginRequest;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.PasswordResetRequest;
import com.github.auth.domain.service.AccountService;
import com.github.auth.domain.service.PasswordService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/auth/")
@Tag(name = "Auth Controller", description = "Auth API")
@Validated
public class AuthController {
    private final AccountService accountService;
    private final PasswordService passwordService;

    @Operation(summary = "User registration")
    @PostMapping("/signup")
    public AuthResponse signup(@Validated @RequestBody AccountRequest request) {
        return accountService.register(request);
    }

    @Operation(summary = "User authorization")
    @PostMapping("/signin")
    public AuthResponse signin(@Validated @RequestBody LoginRequest request) {
        return accountService.login(request);
    }

    @Operation(summary = "User signout")
    @DeleteMapping("/signout/{userid}")
    public void signout(@PathVariable("userid") @Validated String userid) {
        accountService.revokeToken(userid);
    }

    @Operation(summary = "User password reset")
    @PostMapping("/reset-password")
    public AuthResponse resetPassword(@Validated @RequestBody EmailMessage email) {
        return passwordService.sendResetPasswordLink(email);
    }

    @Operation(summary = "User password change")
    @PatchMapping("/change-password")
    public AuthResponse changePassword(@RequestBody PasswordResetRequest passwordResetRequest,
                                       @RequestParam("token") String resetToken) {
        return passwordService.changePasswordByToken(passwordResetRequest, resetToken);
    }
}
