package com.github.store.domain.service;

import com.github.store.domain.model.Product;
import org.springframework.stereotype.Service;
import reactor.core.publisher.Flux;

@Service
public interface ProductService {
   Flux<Product> getAllProducts();
}
