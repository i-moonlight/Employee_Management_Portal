package com.ecommerce.store.domain.dto;

import lombok.*;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;

@Data
@AllArgsConstructor
@Builder
@FieldDefaults(level = AccessLevel.PRIVATE)
public class Response<T> {
    int           code;
    HttpStatus    status;
    String        message;
    Integer       numProduct;
    T             body;

    public Response(int code, HttpStatus status, String message, T body) {
        this.code = code;
        this.status = status;
        this.message = message;
        this.body = body;
    }
}
