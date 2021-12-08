package com.github.store.dao.repository;

import com.github.store.domain.model.Product;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;
import org.springframework.data.r2dbc.repository.Query;
import org.springframework.data.repository.reactive.ReactiveCrudRepository;
import org.springframework.stereotype.Repository;
import reactor.core.publisher.Flux;

@Repository
@EnableJpaRepositories
public interface StoreRepository extends ReactiveCrudRepository<Product, Integer> {

   //public Flux<Product> findAll();

//   @Query("SELECT * FROM product ")
//   Flux<Product> findAll(@Param("id") Long id);

   @Query("SELECT * FROM product")
   Flux<Product> findAll();
}