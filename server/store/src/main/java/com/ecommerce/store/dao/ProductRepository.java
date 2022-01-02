package com.ecommerce.store.dao;

import com.ecommerce.store.entity.Product;
import org.springframework.data.r2dbc.repository.*;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import reactor.core.publisher.Mono;

import java.util.UUID;

@Repository
public interface ProductRepository extends R2dbcRepository<Product, UUID> {

    @Query("SELECT * FROM product p WHERE p.id = :id")
    Mono<Product> getProductById(@Param("id") UUID id);

}

