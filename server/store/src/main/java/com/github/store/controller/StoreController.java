package com.github.store.controller;

import com.github.store.domain.service.ProductService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins = "http://localhost:8080")
@RestController
@RequiredArgsConstructor
@RequestMapping("/api/store/")
public class StoreController {
   private final ProductService storeService;

   @GetMapping("/test")
   @ResponseStatus(HttpStatus.OK)
   public String getAll() {
      return "this.storeService.getAllProducts()";
   }
}
