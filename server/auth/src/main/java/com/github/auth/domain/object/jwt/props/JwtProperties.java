package com.github.auth.domain.object.jwt.props;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

/**
 * Token expiration properties
 */
@Component
@ConfigurationProperties(prefix = "security.jwt.expiration")
@Data
public class JwtProperties {
    private String secret;
    private long expireAccessToken;
    private long expireRefreshToken;
}
