package com.ecommerce.store.domain.service;

import com.ecommerce.store.entity.Product;
import reactor.core.publisher.*;

import java.util.UUID;

public interface ProductService {
    Flux<Product> findProductAll();
    Mono<Product> findProductById(UUID productId);
}
