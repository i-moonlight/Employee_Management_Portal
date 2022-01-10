package com.ecommerce.store.domain.object.product;

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
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RepositoryProductService implements ProductService {
    private final ProductRepository repository;

    @Override
    public Flux<Product> findProductList() {
        return repository.findAll().delayElements(Duration.ofSeconds(1));
    }

    @Override
    public Mono<Response<Product>> findProductById(UUID id) {
        return repository.getProductById(id)
                .map(product ->
                        new Response<>(HttpStatus.OK, "Product found", product))
                .switchIfEmpty(Mono.just(
                        new Response<>(HttpStatus.NOT_FOUND, "Product not found", null)))
                .onErrorResume(DatabaseException.class, ex -> Mono.just(
                        new Response<>(HttpStatus.INTERNAL_SERVER_ERROR, "Database error", null))
                );
    }

    @Override
    public Mono<Product> createProduct(Product product) {
        return repository.createProduct(
                product.getBrand(),
                product.getCategory(),
                product.getDescription(),
                product.getImage()
        );
    }
}
