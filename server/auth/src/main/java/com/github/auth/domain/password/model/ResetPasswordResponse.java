package com.github.auth.domain.password.model;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@Builder
@Data
@NoArgsConstructor
public class ResetPasswordResponse {
    private Integer status;
    private String text;
}
