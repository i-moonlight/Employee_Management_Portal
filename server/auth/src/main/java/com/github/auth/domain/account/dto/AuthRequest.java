package com.github.auth.domain.account.dto;

import lombok.Builder;
import lombok.Data;

@Builder
@Data
public class AuthRequest {
    private String firstname;
    private String lastname;
    private String username;
    private String email;
    private String password;
}
