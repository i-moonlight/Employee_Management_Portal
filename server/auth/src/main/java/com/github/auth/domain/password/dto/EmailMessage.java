package com.github.auth.domain.password.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import lombok.Data;

@Data
public class EmailMessage {
    @Schema(description = "email", example = "jonnydeep@gmail.com")
    @NotBlank(message = "Email should not be empty")
    @Email(message = "Email is not valid",
            regexp = "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    private String to;
}
