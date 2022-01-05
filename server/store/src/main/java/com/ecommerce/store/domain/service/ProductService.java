package com.ecommerce.store.domain.service;

import com.ecommerce.store.entity.Product;
import org.springframework.stereotype.Component;
import reactor.core.publisher.*;

import java.util.UUID;

@Component
public interface ProductService {
    Flux<Product> findProductList();
    Mono<Product> findProductById(UUID productId);
}
