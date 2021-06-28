package com.github.auth.domain.password.dto;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@Data
@NoArgsConstructor
public class ResetPasswordRequest {
    private String accessToken;

    @NotBlank(message = "Email should not be empty")
    @Email(message = "Email is not valid",
            regexp = "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    private String email;

    @NotBlank(message = "New password should not be empty")
    @Pattern(message = "New password is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,16}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 16, message = "Password should be between 8 and 16 characters")
    private String newPassword;
}