package com.github.auth.domain.account.dto;

import jakarta.validation.constraints.*;
import lombok.Builder;
import lombok.Data;

@Builder
@Data
public class AuthRequest {
    @NotBlank(message = "Firstname should not be empty")
    @Size(min = 3, max = 30, message = "Firstname should be between 3 and 30 characters")
    private String firstname;

    @NotBlank(message = "Lastname should not be empty")
    @Size(min = 3, max = 30, message = "Lastname should be between 3 and 30 characters")
    private String lastname;

    @NotBlank(message = "Username should not be empty")
    @Size(min = 3, max = 30, message = "Username should be between 3 and 30 characters")
    private String username;

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
     * пароль состоит из 8-16 символов без пробелов
     */
    @NotBlank(message = "Password should not be empty")
    @Pattern(message = "Password is not valid",
              regexp = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\s:])(\\S){8,16}$",
               flags = Pattern.Flag.CASE_INSENSITIVE)
    @Size(min = 8, max = 16, message = "Password should be between 8 and 16 characters")
    private String password;
}
