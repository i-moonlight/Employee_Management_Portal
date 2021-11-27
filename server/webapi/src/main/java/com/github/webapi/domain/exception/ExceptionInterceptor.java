package com.github.webapi.domain.exception;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@RestControllerAdvice
public class ExceptionInterceptor {

   @ExceptionHandler(ImageUploadException.class)
   @ResponseStatus(HttpStatus.BAD_REQUEST)
   public ExceptionBody handleImageUpload(final ImageUploadException e) {
      return new ExceptionBody(HttpStatus.BAD_REQUEST.value(), e.getMessage());
   }
}
