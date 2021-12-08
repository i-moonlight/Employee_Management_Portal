package com.github.store.domain.object.image;

import com.github.store.dao.repository.StoreRepository;
import com.github.store.domain.model.Product;
import com.github.store.domain.service.ProductService;
import lombok.NoArgsConstructor;
import org.springframework.stereotype.Service;
import reactor.core.publisher.Flux;

@Service
@NoArgsConstructor
public class StoreService implements ProductService {

   private StoreRepository storeRepository;

   public StoreService(StoreRepository storeRepository) {
      this.storeRepository = storeRepository;
   }


//   public Flux<Product> getAllProducts(String name) {
//      return (name != null)
//              ? storeRepository.findByName(name)
//              : storeRepository.findAll();
//   }

   public Flux<Product> getAllProducts(){
      return this.storeRepository.findAll();
   }

}
