package com.ecommerce.store.domain.object.product;

import com.ecommerce.store.domain.service.ProductService;
import com.ecommerce.store.entity.Product;
import com.ecommerce.store.dao.ProductRepository;
import lombok.RequiredArgsConstructor;
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
    public Mono<Product> findProductById(UUID id) {
        return repository.getProductById(id);
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
