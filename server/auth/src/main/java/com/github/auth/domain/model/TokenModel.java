package com.github.auth.domain.model;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class Token {
    public Integer id;
    public String token;
    public String tokenType;
    public boolean revoked;
    public boolean expired;
    public User user;
}
