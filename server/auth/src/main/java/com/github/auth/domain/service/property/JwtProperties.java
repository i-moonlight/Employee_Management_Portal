package com.github.auth.domain.service.property;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@Component
@ConfigurationProperties(prefix = "security.jwt.expiration")
@Data
public class JwtProperties {
    private String secret;
    private long accessToken;
    private long refreshToken;
}
