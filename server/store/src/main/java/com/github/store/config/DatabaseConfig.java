package com.github.store.config;

import io.r2dbc.spi.ConnectionFactories;
import io.r2dbc.spi.ConnectionFactory;
import io.r2dbc.spi.ConnectionFactoryOptions;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.r2dbc.config.AbstractR2dbcConfiguration;

import static io.r2dbc.spi.ConnectionFactoryOptions.*;

@Configuration
public class DatabaseConfig extends AbstractR2dbcConfiguration {

//   @Bean
//   public ConnectionFactory connectionFactory() {
//      return new H2ConnectionFactory(H2ConnectionConfiguration.builder()
//              .url("r2dbc:h2:mem:///test?options=DB_CLOSE_DELAY=-1;DB_CLOSE_ON_EXIT=FALSE")
//              .build());
//   }

//   @Bean
//   public R2dbcEntityTemplate r2dbcEntityTemplate() {
//      return new R2dbcEntityTemplate(connectionFactory());
//   }

//   @Override
//   public ConnectionFactory connectionFactory() {
//      return null;
//   }

   @Bean
   public ConnectionFactory connectionFactory() {
      return ConnectionFactories.get(
              ConnectionFactoryOptions.builder()
                      .option(DRIVER, "postgresql")
                      .option(HOST, "localhost")
                      .option(PORT, 5432)
                      .option(USER, "admin")
                      .option(PASSWORD, "52670")
                      .option(DATABASE, "auth")
//                      .option(MAX_SIZE, 40)
                      .build());
   }

//   public static ConnectionFactory  connectionFactory() {
//      return new PostgresqlConnectionFactory(PostgresqlConnectionConfiguration
//              .builder()
//              .host("localhost")
//              .database("employee")
//              .username("postgres")
//              .password("nssdw")
//              .port(5432)
//              .build());
//   }
}
