package com.ecommerce.store.entity;

import com.fasterxml.jackson.annotation.JsonFormat;
import jakarta.persistence.*;
import lombok.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.UUID;

@AllArgsConstructor
@Builder
@Data
@Entity
@NoArgsConstructor
@Table(name = "product")
//@FieldDefaults(level = AccessLevel.PRIVATE)
public class Product {
    static final String DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    @Id
    @Column(name = "id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    UUID       id;
    String     brand;
    String     category;
    String     description;
    String     image;
    BigDecimal price;
    float      rating;
    int        numReviews;
    int        countStock;

    @JsonFormat(pattern = DATE_TIME_FORMAT)
    LocalDateTime createdAt;

    @JsonFormat(pattern = DATE_TIME_FORMAT)
    LocalDateTime updatedAt;

    public Product(UUID id, String brand, String category, String description, String image) {
        this.id = id;
        this.brand = brand;
        this.category = category;
        this.description = description;
        this.image = image;
    }
}
