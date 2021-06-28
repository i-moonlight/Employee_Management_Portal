package com.github.auth.domain.account.props;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

/**
 *  Свойства истечения срока токена
 */
@Component
@ConfigurationProperties(prefix = "security.jwt.expiration")
@Data
public class JwtProperties {
    private String secret;
    private long accessToken;
    private long refreshToken;
}
