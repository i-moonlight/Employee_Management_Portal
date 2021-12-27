package com.ecommerce.store.controller;

import com.ecommerce.store.entity.Product;
import com.ecommerce.store.repository.ProductRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.Flux;

@CrossOrigin(origins = "http://localhost:9001")
@RestController
@RequestMapping("/api/product")
@RequiredArgsConstructor
public class ProductController {
    private final ProductRepository service;

    @GetMapping("/all") // http://localhost:9001/api/product/all
    public Flux<Product> getProducts(){
        return service.findAll();
    }
}
