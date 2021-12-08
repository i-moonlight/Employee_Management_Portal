package com.github.store.domain.model;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.*;
import org.springframework.data.relational.core.mapping.Table;

import java.io.Serializable;

@Entity
@Table (name = "product")
@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
@With
public class Product implements Serializable {
   @Id
   private int id;
   
   private String name;
   
//   private String category;
//   private String image;
//   private int price;
//   private String brand;
//   private float rating;
//   private int numReviews;
//   private int countInStock;

}
