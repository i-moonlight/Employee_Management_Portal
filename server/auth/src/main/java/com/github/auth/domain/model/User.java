package com.github.auth.domain.model;

import lombok.Builder;
import lombok.Data;

@Builder
@Data
public class User {
    private int id;
    private String username;
    private String firstname;
    private String lastname;
    private String email;
    private String password;
    private Role role;
}
