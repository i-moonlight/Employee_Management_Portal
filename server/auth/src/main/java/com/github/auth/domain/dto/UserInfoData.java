package com.github.auth.domain.dto;

import lombok.Builder;
import lombok.Data;

import java.util.UUID;

@Builder
@Data
public class UserInfoData {
    private UUID id;
    private String username;
    private String firstname;
    private String lastname;
    private String email;
    private final boolean isActivated = false;
}
