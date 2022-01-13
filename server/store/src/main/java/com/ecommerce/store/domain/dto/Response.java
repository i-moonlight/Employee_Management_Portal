package com.ecommerce.store.domain.dto;

import com.ecommerce.store.entity.Product;
import lombok.*;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;

import java.util.List;

@Data
@AllArgsConstructor
@Builder
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class Response {
    HttpStatus    status;
    String        message;
    List<Product> body;
}
