package com.ecommerce.store.domain.exeption;

public class ImageNotFoundException extends RuntimeException {
    public ImageNotFoundException(String message, Throwable cause) {
        super(message, cause);
    }
}
