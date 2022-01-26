package com.ecommerce.store.domain.service;

import com.ecommerce.store.domain.model.Image;
import org.springframework.http.codec.multipart.FilePart;
import org.springframework.stereotype.Component;
import reactor.core.publisher.Mono;

import java.util.UUID;

@Component
public interface ImageService {
    Mono<Boolean> saveFileToMinio(FilePart filePart);
    String upload(Image image);
    void uploadImage(UUID id, Image image);
}
