package com.github.auth.domain.object.account.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.Builder;
import lombok.Data;

@Builder
@Data
@Schema(description = "Request on user login")
public class LoginRequest {
    @Schema(description = "username", example = "username")
    @NotBlank(message = "Username should not be empty")
    @Size(min = 3, max = 30, message = "Username should be between 3 and 30 characters")
    private String username;

    @Schema(description = "password", example = "123456")
    @NotBlank(message = "Password should not be empty")
    @Pattern(message = "Password is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,30}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 30, message = "Password should be between 8 and 30 characters")
    private String password;
}
