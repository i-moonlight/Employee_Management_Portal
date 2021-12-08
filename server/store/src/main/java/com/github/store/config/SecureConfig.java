package com.github.store.config;

import com.github.store.config.props.MinioProperties;
import io.minio.MinioClient;
import io.swagger.v3.oas.models.Components;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.security.SecurityRequirement;
import io.swagger.v3.oas.models.security.SecurityScheme;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Lazy;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor(onConstructor = @__(@Lazy))
//@EnableWebFluxSecurity
//@EnableReactiveMethodSecurity
public class SecureConfig {

   private final MinioProperties minioProperties;
   private final SecurityAuthFilter securityAuthFilter;

   @Bean
   public PasswordEncoder passwordEncoder() {
      return new BCryptPasswordEncoder();
   }

   @Bean
   public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
      return http.csrf().disable()
              .authorizeHttpRequests()
              .requestMatchers(
                      "/api/store/test",
                      "/api/auth/signin",
                      "/api/auth/signup",
                      "/api/auth/token",
                      "/api/auth/refresh",
                      "/api/auth/signout/**",
                      "/api/auth/forgot-password/**",
                      "/api/auth/reset-password/**")
              .permitAll().and()
              .authorizeHttpRequests()
              .requestMatchers("/products/**").permitAll().and()
              .authorizeHttpRequests()
              .requestMatchers(
                      "/api/v1/auth/**",
                      "/swagger-ui/**",
                      "/v3/api-docs/**").permitAll()
              .anyRequest()
              .authenticated().and()
              .sessionManagement()
              .sessionCreationPolicy(SessionCreationPolicy.STATELESS).and()
              .addFilterBefore(securityAuthFilter, UsernamePasswordAuthenticationFilter.class)
              .build();
   }

   @Bean
   public OpenAPI openAPI() {
      return new OpenAPI().addSecurityItem(new SecurityRequirement().addList("bearerAuth"))
              .components(new Components()
                      .addSecuritySchemes("bearerAuth", new SecurityScheme()
                      .type(SecurityScheme.Type.HTTP)
                      .scheme("bearer")
                      .bearerFormat("JWT")))
              .info(new Info()
                      .title("Task list API")
                      .description("Auth application")
                      .version("1.0")
              );
   }

   @Bean
   public MinioClient minioClient() {
      return MinioClient.builder()
              .endpoint("http://127.0.0.1:9000/")
              .credentials(minioProperties.getAccessKey(), minioProperties.getSecretKey())
              .build();
   }
}
