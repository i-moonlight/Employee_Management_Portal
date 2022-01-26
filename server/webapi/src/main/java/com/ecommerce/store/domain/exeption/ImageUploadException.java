package com.ecommerce.store.domain.exeption;

public class ImageUploadException extends RuntimeException {
    public ImageUploadException(final String message) {
        super(message);
    }
}
