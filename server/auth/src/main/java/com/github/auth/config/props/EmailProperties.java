package com.github.auth.config.props;

import lombok.Data;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.stereotype.Component;

@Component
@ConfigurationProperties(prefix = "spring.mail")
@Data
public class EmailProperties {
    private int port;
    private String host;
    private String protocol;
    private String username;
    private String password;
}
