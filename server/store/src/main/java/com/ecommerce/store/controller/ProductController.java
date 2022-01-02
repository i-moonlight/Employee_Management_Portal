package com.ecommerce.store.controller;

import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.*;

import java.util.UUID;

@CrossOrigin(origins = "http://localhost:9001")
@RestController
@RequestMapping("/api/product")
@RequiredArgsConstructor
public class ProductController {
    private final ProductService service;

    @GetMapping("/all") // http://localhost:9001/api/product/all
    public Flux<Product> getProductAll(){
        return service.findProductAll();
    }

    @GetMapping("/{id}")
    Mono<Product> getProductById(@PathVariable("id") UUID productId) {
        return service.findProductById(productId);
    }
}
