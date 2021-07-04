package com.github.auth.domain.model;

import lombok.Builder;
import lombok.Data;

import java.util.UUID;

@Builder
@Data
public class User {
    private UUID id;
    private String username;
    private String firstname;
    private String lastname;
    private String email;
    private String password;
    private Role role;
}
