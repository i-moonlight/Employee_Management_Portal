package com.github.auth.domain.object.account.dto;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.Builder;
import lombok.Data;

@Builder
@Data
@Schema(description = "Request for create account")
public class AccountRequest {

    @Schema(description = "firstname", example = "jonny")
    @NotBlank(message = "Firstname should not be empty")
    @Size(min = 3, max = 30, message = "Firstname should be between 3 and 30 characters")
    private String firstname;

    @Schema(description = "lastname", example = "deep")
    @NotBlank(message = "Lastname should not be empty")
    @Size(min = 3, max = 30, message = "Lastname should be between 3 and 30 characters")
    private String lastname;

    @Schema(description = "username", example = "jonnydeep")
    @NotBlank(message = "Username should not be empty")
    @Size(min = 3, max = 30, message = "Username should be between 3 and 30 characters")
    private String username;

    @Schema(description = "email", example = "username@gmail.com")
    @NotBlank(message = "Email should not be empty")
    @Email(message = "Email is not valid",
            regexp = "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,3}",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    private String email;

    /**
     * Это регулярное выражение совпадает только тогда, когда все следующие значения верны:
     * пароль должен содержать 1 число (0-9)
     * пароль должен содержать 1 заглавную букву
     * пароль должен содержать 1 строчную букву
     * пароль должен содержать 1 небуквенно-цифровое число
     * пароль состоит из 8-30 символов без пробелов
     */
    @Schema(description = "password", example = "123456789")
    @NotBlank(message = "Password should not be empty")
    @Pattern(message = "Password is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,30}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 30, message = "Password should be between {min} and {min} characters")
    private String password;

    @Schema(description = "Password confirm", example = "123456789")
    @NotBlank(message = "Password confirm should not be empty")
    @Pattern(message = "Password confirm is not valid",
            regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,30}$",
            flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 30, message = "Password should be between {min} and {min} characters")
    private String passwordConfirm;
}
