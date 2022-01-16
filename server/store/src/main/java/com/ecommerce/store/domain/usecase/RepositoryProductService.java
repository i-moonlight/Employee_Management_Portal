package com.ecommerce.store.domain.usecase;

import com.ecommerce.store.dao.ProductRepository;
import com.ecommerce.store.domain.dto.Response;
import com.ecommerce.store.domain.exeption.DatabaseException;
import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import reactor.core.publisher.*;

import java.time.Duration;
import java.util.*;

@Service
@RequiredArgsConstructor
public class RepositoryProductService implements ProductService {
    private final ProductRepository repository;

    @Override
    public Mono<Response> findProductList() {
        return repository.findAll().delayElements(Duration.ofSeconds(1))
                .collectList()
                .flatMap(productList -> {
                    if (productList.isEmpty()) {
                        return Mono.just(new Response(
                                200, HttpStatus.OK, "List is empty", productList)
                        );
                    } else {
                        return Mono.just(new Response(
                                200, HttpStatus.OK, "List is found", productList)
                        );
                    }
                })
                .switchIfEmpty(Mono.just(new Response(
                        404, HttpStatus.NOT_FOUND, "Product not found", null))
                )
                .onErrorResume(DatabaseException.class, ex -> Mono.just(new Response(
                        500, HttpStatus.INTERNAL_SERVER_ERROR, "Database error", null))
                );
    }

    @Override
    public Mono<Response> findProductById(UUID id) {
        return repository.getProductById(id)
                .map(product -> new Response(
                        200, HttpStatus.OK, "Product found", (List<Product>) product)
                )
                .switchIfEmpty(Mono.just(new Response(
                        404, HttpStatus.NOT_FOUND, "Product not found", null))
                )
                .onErrorResume(DatabaseException.class, ex -> Mono.just(new Response(
                        500, HttpStatus.INTERNAL_SERVER_ERROR, "Database error", null))
                );
    }

    @Override
    public Mono<Response> createProduct(Product p) {
        return repository.createProduct(p.getBrand(), p.getCategory(), p.getDescription(), p.getImage())
                .map(result -> new Response(
                        201, HttpStatus.CREATED, result, null)
                )
                .onErrorResume(ex -> Mono.just(new Response(
                        500, HttpStatus.INTERNAL_SERVER_ERROR, "Error creating product: " + ex.getMessage(), null))
                );
    }
}
