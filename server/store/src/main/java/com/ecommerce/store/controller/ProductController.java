package com.ecommerce.store.controller;

import com.ecommerce.store.domain.dto.Response;
import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.*;

import java.util.UUID;

@CrossOrigin(origins = "http://localhost:9001")
@RestController
@RequestMapping("/api/product")
@RequiredArgsConstructor
public class ProductController {
    private final ProductService service;

    @GetMapping("/list") // http://localhost:9001/api/product/list
    @ResponseStatus(HttpStatus.OK)
    public Mono<Response> getProductList() {
        return service.findProductList();
    }

    @GetMapping("/{id}")
    @ResponseStatus(HttpStatus.OK)
    public Mono<Response> getProductById(@PathVariable("id") UUID productId) {
        return service.findProductById(productId);
    }

    @PostMapping("/create")
    @ResponseStatus(HttpStatus.CREATED)
    public Mono<Response> createProduct(@RequestBody Product product) {
        return service.createProduct(product);
    }

    @PutMapping("/update/{productId}")
    public Mono<Response> updateProduct(@PathVariable UUID productId, @RequestBody Product updatedProduct) {
        return service.updateProductById(productId, updatedProduct);
    }
}
