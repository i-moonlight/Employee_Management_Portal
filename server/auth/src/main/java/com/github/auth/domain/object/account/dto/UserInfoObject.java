package com.github.auth.domain.object.account.dto;

import lombok.Builder;
import lombok.Data;

import java.util.UUID;

@Builder
@Data
public class UserInfoObject {
    private UUID id;
    private String username;
    private String firstname;
    private String lastname;
    private String email;
    private final boolean isActivated = false;
}
