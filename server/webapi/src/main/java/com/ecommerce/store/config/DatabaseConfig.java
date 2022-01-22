package com.ecommerce.store.config;

import org.flywaydb.core.Flyway;
import org.springframework.boot.autoconfigure.flyway.FlywayProperties;
import org.springframework.boot.autoconfigure.r2dbc.R2dbcProperties;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.*;

@Configuration
@EnableConfigurationProperties({R2dbcProperties.class, FlywayProperties.class})
class DatabaseConfig {

    @Bean(initMethod = "migrate")
    public Flyway flyway(FlywayProperties flyway, R2dbcProperties r2dbc) {
        return Flyway.configure()
                .dataSource(flyway.getUrl(), r2dbc.getUsername(), r2dbc.getPassword())
                .locations(flyway.getLocations().stream().toArray(String[]::new))
                .baselineOnMigrate(true)
                .load();
    }
}
