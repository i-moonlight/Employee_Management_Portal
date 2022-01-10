package com.ecommerce.store.domain.service;

import com.ecommerce.store.domain.dto.Response;
import com.ecommerce.store.entity.Product;
import org.springframework.stereotype.Component;
import reactor.core.publisher.*;

import java.util.UUID;

@Component
public interface ProductService {
    Flux<Product> findProductList();
    Mono<Response<Product>> findProductById(UUID productId);
    Mono<Product> createProduct(Product product);
}
