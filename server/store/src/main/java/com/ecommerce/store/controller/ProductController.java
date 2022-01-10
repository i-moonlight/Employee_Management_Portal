package com.ecommerce.store.controller;

import com.ecommerce.store.domain.dto.Response;
import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.reactive.function.server.ServerRequest;
import org.springframework.web.reactive.function.server.ServerResponse;
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
    public Flux<Product> getProductList(){
        return service.findProductList();
    }

    @GetMapping("/{id}")
    @ResponseStatus(HttpStatus.OK)
    public Mono<Response<Product>> getProductById(@PathVariable("id") UUID productId) {
        return service.findProductById(productId);
    }

    @PostMapping(value = "/create", consumes = "application/json")
    @ResponseStatus(HttpStatus.CREATED)
    public Mono<Product> createProduct(@RequestBody Product product) {
        return service.createProduct(product);
    }
}
