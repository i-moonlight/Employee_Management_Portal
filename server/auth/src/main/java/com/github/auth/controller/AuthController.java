package com.github.auth.controller;

import com.github.auth.domain.exeption.response.ExceptionBody;
import com.github.auth.domain.exeption.response.ValidationErrorResponse;
import com.github.auth.domain.object.account.dto.AccountRequest;
import com.github.auth.domain.object.account.dto.AuthResponse;
import com.github.auth.domain.object.account.dto.LoginRequest;
import com.github.auth.domain.object.jwt.dto.JwtRequest;
import com.github.auth.domain.object.password.dto.EmailMessage;
import com.github.auth.domain.object.password.dto.PasswordResetRequest;
import com.github.auth.domain.service.AccountService;
import com.github.auth.domain.service.PasswordService;
import com.github.auth.domain.service.TokenService;
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
   private final TokenService jwtService;

   @Operation(summary = "User registration",
           description = "The endpoint accepts a AccountRequest with all fields" +
                   " returns a AuthResponse with new tokens.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = AuthResponse.class), examples =
                   @ExampleObject())),

                   @ApiResponse(responseCode = "400", description = "Invalid client request format.", content =
                   @Content(mediaType = "application/json", examples =
                   @ExampleObject(value = "{" +
                           "\"status\":400," +
                           "\"message\":\"User username already exists\"," +
                           "\"accessToken\":null," +
                           "\"refreshToken\":null," +
                           "\"userInfoObject\":null}"
                   ))),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = ExceptionBody.class), examples =
                   @ExampleObject(value = "{" +
                           "\"status\":500," +
                           "\"message\":" +
                           "\"Internal error\"," +
                           "\"errors\":null}"
                   )))
           })
   @PostMapping("/signup")
   public AuthResponse signup(@Validated @RequestBody AccountRequest request) {
      return accountService.register(request);
   }

   @Operation(summary = "User authorization",
           description = "The endpoint accepts a LoginRequest with a username and password fields" +
                   " returns a AuthResponse with new tokens.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = AuthResponse.class), examples =
                   @ExampleObject())),

                   @ApiResponse(responseCode = "400", description = "Invalid client request format.", content =
                   @Content(mediaType = "application/json", examples =
                   @ExampleObject(value = "{" +
                           "\"status\":400,\"" +
                           "message\":\"Validation failed\"," +
                           "\"violations\":[{\"fieldName\":\"username\"," +
                           "\"message\":\"Username should be between 3 and 30 characters\"},{\"" +
                           "fieldName\":\"password\"," +
                           "\"message\":\"Password should be between 8 and 30 characters\"},{\"" +
                           "fieldName\":\"password\"," +
                           "\"message\":\"Password is not valid\"}]}"
                   ))),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = ExceptionBody.class)))
           })
   @PostMapping("/signin")
   public AuthResponse signin(@Validated @RequestBody LoginRequest request) {
      return accountService.login(request);
   }

   @Operation(summary = "User signout",
           description = "The endpoint accepts a user id and to delete refresh token and redirect on login page.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema())),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema()))
           })
   @DeleteMapping("/signout/{userid}")
   public void signout(@Validated @PathVariable("userid") String userid) {
      jwtService.deleteRefreshToken(userid);
   }

   @Operation(summary = "User reset password by email",
           description = "The endpoint accepts a PasswordResetRequest with a newPassword and passwordConfirm fields" +
                   " returns a AuthResponse with new tokens.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = AuthResponse.class), examples =
                   @ExampleObject(value = "{" +
                           "\"status\":200," +
                           "\"message\":\"Please, check your email for password reset\"," +
                           "\"accessToken\":null," +
                           "\"refreshToken\":null," +
                           "\"userInfoObject\":null}"
                   ))),

                   @ApiResponse(responseCode = "400", description = "Invalid client request format.", content =
                   @Content(mediaType = "application/json", examples =
                   @ExampleObject(value = "{" +
                           "\"status\":400," +
                           "\"message\":\"Validation failed\"," +
                           "\"violations\":[{" +
                           "\"fieldName\":\"to\"," +
                           "\"message\":\"Email should not be empty\"},{" +
                           "\"field Name\":\"to\",\"message\":\"Email is not valid\"}]}"
                   ))),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema()))
           })
   @PostMapping("/reset-password")
   public AuthResponse resetPassword(@Validated @RequestBody EmailMessage email) {
      return passwordService.sendResetPasswordLink(email);
   }

   @Operation(summary = "User password change",
           description = "The endpoint accepts a PasswordResetRequest with a newPassword and passwordConfirm fields" +
                   " returns a AuthResponse with new tokens.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = AuthResponse.class))),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema()))
           })
   @PatchMapping("/change-password")
   public AuthResponse changePassword(@RequestBody PasswordResetRequest passwordResetRequest,
                                      @RequestParam("token") String resetToken) {
      return passwordService.changePasswordByToken(passwordResetRequest, resetToken);
   }

   @Operation(summary = "Get new access token",
           description = "The endpoint accepts a RefreshJwtRequest with a single refreshToken field and" +
                   " returns a JwtResponse with a new access token.",
           responses = {
                   @ApiResponse(responseCode = "200", description = "Request completed successfully.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema(implementation = AuthResponse.class))),

                   @ApiResponse(responseCode = "400", description = "Invalid client request format.", content =
                   @Content(mediaType = "application/json", examples =
                   @ExampleObject(value = "{" +
                           "\"status\":400," +
                           "\"message\":\"Refresh token is not valid\"," +
                           "\"accessToken\":\"null\"," +
                           "\"refreshToken\":\"null\"," +
                           "\"userInfoObject\":\"null\"}"
                   ))),

                   @ApiResponse(responseCode = "500", description = "Server error.", content =
                   @Content(mediaType = "application/json", schema =
                   @Schema()))
           })
   @GetMapping("/token")
   public AuthResponse getNewAccessToken(@Validated @RequestBody JwtRequest request) {
      return jwtService.getAccessToken(request.getRefreshToken());
   }

   @Operation(summary = "Get new access and refresh tokens",
           description = "The endpoint accepts a RefreshJwtRequest with a single refreshToken field and" +
                   " returns a JwtResponse with new tokens.",
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
      return jwtService.getRefreshToken(request.getRefreshToken());
   }
}
