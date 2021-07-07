package com.github.auth.domain.password.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@Data
@Schema(description = "Request for reset password")
@NoArgsConstructor
public class PasswordResetRequest {

    @Schema(description = "email", example = "jonnydeep@gmail.com")
    @NotBlank(message = "Email should not be empty")
    @Email(message = "Email is not valid",
            regexp = "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    private String email;

    @Schema(description = "new password", example = "125535789")
    @NotBlank(message = "New password should not be empty")
    @Pattern(message = "New password is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,30}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 30, message = "Password should be between {min} and {min} characters")
    private String newPassword;

    @Schema(description = "confirm password", example = "125535789")
    @NotBlank(message = "Confirm password should not be empty")
    @Pattern(message = "Confirm password is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,30}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 30, message = "Password should be between {min} and {min} characters")
    private String passwordConfirm;
}
