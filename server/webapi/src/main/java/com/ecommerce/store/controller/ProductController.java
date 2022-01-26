package com.ecommerce.store.controller;

import com.ecommerce.store.domain.dto.Response;
import com.ecommerce.store.domain.model.Image;
import com.ecommerce.store.domain.service.*;
import com.ecommerce.store.entity.Product;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.*;
import org.springframework.http.*;
import org.springframework.http.codec.multipart.FilePart;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;
import reactor.core.publisher.Mono;

import java.util.UUID;

@CrossOrigin(origins = "http://localhost:9001")
@RestController
@RequestMapping("/api/product")
@RequiredArgsConstructor
public class ProductController {
    @Qualifier("repositoryProductService")
    private final ProductService service;
    @Qualifier("storageImageService")
    private final ImageService imageService;

    @GetMapping("/list") // http://localhost:9001/api/product/list
    @ResponseStatus(HttpStatus.OK)
    public Mono<Response> getProductList() {
        return service.findProductList();
    }

    @GetMapping("/{id}")
    @ResponseStatus(HttpStatus.OK)
    public Mono<Response> getProductById(@PathVariable("id") UUID productId) {
        return service.findProductById(productId);
    }

    @PostMapping("/create")
    @ResponseStatus(HttpStatus.CREATED)
    public Mono<Response> createProduct(@RequestBody Product product) {
        return service.createProduct(product);
    }

    @PutMapping("/update/{productId}")
    public Mono<Response> updateProduct(@PathVariable UUID productId, @RequestBody Product updatedProduct) {
        return service.updateProductById(productId, updatedProduct);
    }

    @DeleteMapping("/delete/{productId}")
    public Mono<Response> deleteProduct(@PathVariable UUID productId) {
        return service.deleteProductById(productId);
    }
    @PostMapping(
            value = "/upload",
            consumes = MediaType.MULTIPART_FORM_DATA_VALUE
    )
    public Mono<Boolean> uploadImage(@RequestPart("file") FilePart filePart) {
        return imageService.saveFileToMinio(filePart);
    }

    @PostMapping("/{id}/image")
    public void uploadImage(@PathVariable UUID id, @Validated @ModelAttribute Image image) {
        imageService.uploadImage(id, image);
    }
}
