package com.ecommerce.store.domain.usecase;

import com.ecommerce.store.dao.ProductRepository;
import com.ecommerce.store.domain.exeption.ImageUploadException;
import com.ecommerce.store.domain.model.Image;
import com.ecommerce.store.domain.service.ImageService;
import com.ecommerce.store.props.MinioProperties;
import io.minio.*;
import lombok.*;
import lombok.extern.slf4j.Slf4j;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.http.codec.multipart.FilePart;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.multipart.MultipartFile;
import reactor.core.publisher.Mono;

import java.io.InputStream;
import java.util.UUID;

@Slf4j
@Service
@RequiredArgsConstructor
public class StorageImageService implements ImageService {

    private final MinioProperties minioProperties;
    private final MinioClient minioClient;
    private final ProductRepository productRepository;


//    public Mono<Boolean> saveFileToMinio(FilePart filePart) {
//        log.info("About to save database to minio container...");
//        Mono<Boolean> result = Mono.from(filePart.content().flatMap(dataBuffer -> {
//            var bytes = dataBuffer.asByteBuffer().array();
//            dataBuffer.read(bytes);
//            DataBufferUtils.release(dataBuffer);
//            return Flux.just(bytes);
//        })
//                .flatMap(minioClient::write)
//                .then(Mono.just(true))
//                .onErrorMap(throwable -> {
//                    log.error(throwable.getMessage(), throwable);
//                    return throwable;
//                })
//        );
//        log.info("Successfully saved database to minio container...");
//        return result;
//    }

    @Override
    public Mono<Boolean> saveFileToMinio(FilePart filePart) {
        return null;
    }

    @Override
    public String upload(Image image) {
        try {
            createBucket();
        } catch (Exception e) {
            throw new ImageUploadException("Image upload failed: " + e.getMessage());
        }
        MultipartFile file = image.getFile();
        if (file.isEmpty() || file.getOriginalFilename() == null) {
            throw new ImageUploadException("Image must have name.");
        }
        String fileName = generateFileName(file);
        InputStream inputStream;
        try {
            inputStream = file.getInputStream();
        } catch (Exception e) {
            throw new ImageUploadException("Image upload failed: " + e.getMessage());
        }
        saveImage(inputStream, fileName);
        return fileName;
    }

    @SneakyThrows
    private void createBucket() {
        boolean found = minioClient.bucketExists(BucketExistsArgs.builder()
                .bucket(minioProperties.getBucket())
                .build()
        );
        if (!found) {
            minioClient.makeBucket(MakeBucketArgs.builder()
                    .bucket(minioProperties.getBucket())
                    .build()
            );
        }
    }

    private String generateFileName(MultipartFile file) {
        String extension = getExtension(file);
        return UUID.randomUUID() + "." + extension;
    }

    private String getExtension(MultipartFile file) {
        return file.getOriginalFilename().substring(file.getOriginalFilename().lastIndexOf(".") + 1);
    }

    @SneakyThrows
    private void saveImage(InputStream inputStream, String fileName) {
        minioClient.putObject(PutObjectArgs.builder()
                .stream(inputStream, inputStream.available(), -1)
                .bucket(minioProperties.getBucket())
                .object(fileName)
                .build()
        );
    }

    @Override
    @Transactional
    // @CacheEvict(value = "service::getById", key = "#id")
    public void uploadImage(UUID id, Image image) {
        String fileName = upload(image);
        productRepository.addImage(id, fileName);
    }
}
