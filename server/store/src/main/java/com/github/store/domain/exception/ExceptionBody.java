package com.github.store.domain.exception;

import lombok.Data;

import java.util.Map;

@Data
public class ExceptionBody {
    private Integer status;
    private String message;
    private Map<String, String> errors;

    public ExceptionBody(Integer status, String message) {
        this.status = status;
        this.message = message;
    }
}
