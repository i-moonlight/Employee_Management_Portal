package com.ecommerce.store.dao;

import com.ecommerce.store.entity.Product;
import org.springframework.data.r2dbc.repository.*;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import reactor.core.publisher.Mono;

import java.util.UUID;

@Repository
public interface ProductRepository extends R2dbcRepository<Product, UUID> {

    @Query("SELECT * FROM product p WHERE p.id = $1")
    Mono<Product> getProductById(@Param("id") UUID id);

    @Modifying
    @Query("INSERT INTO product (brand, category, description, image)" +
            " VALUES ($1, $2, $3, $4)")
    Mono<Long> createProduct(
            @Param("brand")       String brand,
            @Param("category")    String category,
            @Param("description") String description,
            @Param("image")       String image
    );

    @Modifying
    @Transactional
    @Query("UPDATE product" +
            " SET brand = $1, category = $2, description = $3, image = $4" +
            " WHERE id = $5")
    Mono<Long> updateProduct(
            @Param("brand")       String brand,
            @Param("category")    String category,
            @Param("description") String description,
            @Param("image")       String image,
            @Param("id")          UUID   id
    );

    @Modifying
    @Transactional
    @Query("DELETE FROM Product p WHERE p.id = :productId")
    Mono<Long> deleteProduct(UUID productId);
}

