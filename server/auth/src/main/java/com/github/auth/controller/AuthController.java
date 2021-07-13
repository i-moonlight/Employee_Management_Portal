package com.github.auth.controller;

import com.github.auth.domain.account.dto.AccountRequest;
import com.github.auth.domain.account.dto.AuthResponse;
import com.github.auth.domain.account.dto.JwtRequest;
import com.github.auth.domain.account.dto.LoginRequest;
import com.github.auth.domain.exeption.response.ValidationErrorResponse;
import com.github.auth.domain.password.dto.EmailMessage;
import com.github.auth.domain.password.dto.PasswordResetRequest;
import com.github.auth.domain.service.AccountService;
import com.github.auth.domain.service.PasswordService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.media.ExampleObject;
import io.swagger.v3.oas.annotations.media.Schema;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
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

    @Operation(summary = "Get New Access Token",
            description = "The endpoint accepts a RefreshJwtRequest with a single refreshToken field and returns a JwtResponse with a new access token.",
            responses = {
                    @ApiResponse(responseCode = "200", description = "Request completed successfully", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema(implementation = AuthResponse.class))),
                    @ApiResponse(responseCode = "400", description = "Invalid client request format", content =
                    @Content(mediaType = "application/json", examples =
                    @ExampleObject(value = "{" +
                            "\"status\":400," +
                            "\"message\":\"Refresh token is not valid\"," +
                            "\"accessToken\":\"null\"," +
                            "\"refreshToken\":\"null\"," +
                            "\"userInfoObject\":\"null\"" +
                            "}"))),
                    @ApiResponse(responseCode = "500", description = "Server error", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema()))
            })
    @GetMapping("/token")
    public AuthResponse getNewAccessToken(@Validated @RequestBody JwtRequest request) {
        return accountService.getAccessToken(request.getRefreshToken());
    }

    @Operation(
            summary = "Get new access and refresh tokens",
            description = "The endpoint accepts a RefreshJwtRequest with a single refreshToken field and returns a JwtResponse with new tokens.",
            responses = {
                    @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema(implementation = AuthResponse.class))),

                    @ApiResponse(responseCode = "400", description = "Invalid client request format.", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema(implementation = ValidationErrorResponse.class), examples =
                    @ExampleObject(
                            //name = "review", summary = "External review example",
                            //description = "This example exemplifies the content on our site.",
                            //externalValue = "http://foo.bar/examples/review-example.json",
                            value = "{" +
                                    "\"status\":400," +
                                    "\"message\":\"Validation failed\"," +
                                    "\"violations\":[{\"fieldName\":\"refreshToken\"," +
                                    "\"message\":\"Refresh token can't be blank\"}]}"
                    ))),

                    @ApiResponse(responseCode = "403", description = "Using the token after the expiration date is prohibited.", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema(implementation = AuthResponse.class), examples =
                    @ExampleObject(value = "{" +
                            "\"status\":403," +
                            "\"message\":\"Invalid JWT token.You need to register!\"," +
                            "\"accessToken\":\"null\"," +
                            "\"refreshToken\":\"null\"," +
                            "\"userInfoObject\":\"null\"}"
                    ))),

                    @ApiResponse(responseCode = "500", description = "Server error.", content =
                    @Content(mediaType = "application/json", schema =
                    @Schema()))
            })
    @GetMapping("/refresh")
    public AuthResponse getNewRefreshToken(@Validated @RequestBody JwtRequest request) {
        return accountService.getRefreshToken(request.getRefreshToken());
    }
}
