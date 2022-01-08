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

    @Query("INSERT INTO product (brand, category, description, image)" +
            "VALUES (:brand, :category, :description, :image)")
    Mono<Product> createProduct(
            @Param("brand") String brand,
            @Param("category") String category,
            @Param("description") String description,
            @Param("image") String image
    );
}

