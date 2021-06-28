package com.github.auth.domain.exeption.response;

import lombok.Getter;
import lombok.RequiredArgsConstructor;

import java.util.List;

@Getter
@RequiredArgsConstructor
public class ValidationErrorResponse {
    private final int status;
    private final String message;
    private final List<Violation> violations;
}
