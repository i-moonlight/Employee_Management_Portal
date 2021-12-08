package com.github.store.domain.object.image;

import com.github.store.config.props.MinioProperties;
import com.github.store.domain.exception.ImageUploadException;
import com.github.store.domain.model.Image;
import com.github.store.domain.service.ImageService;
import io.minio.*;
import lombok.*;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.InputStream;
import java.util.*;

@Service
@RequiredArgsConstructor
public class MinioImageService implements ImageService {

   private final MinioClient minioClient;
   private final MinioProperties minioProperties;

   @Override
   public String upload(Image image) {
      try {
         createBucket();
      } catch (Exception e) {
         throw new ImageUploadException("Image upload failed: " + e.getMessage());
      }
      MultipartFile file = image.getFile();
      if (file.isEmpty() || file.getOriginalFilename() == null) {
         throw new ImageUploadException("Image must have name");
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
      boolean aBucket = minioClient
              .bucketExists(BucketExistsArgs.builder()
              .bucket(minioProperties.getBucket())
              .build());

      if (!aBucket) {
         minioClient
                 .makeBucket(MakeBucketArgs.builder()
                 .bucket(minioProperties.getBucket())
                 .build());
      }
   }

   private String generateFileName(final MultipartFile file) {
      String extension = getExtension(file);
      return UUID.randomUUID() + "." + extension;
   }

   private String getExtension(final MultipartFile file) {
      return Objects.requireNonNull(file.getOriginalFilename())
              .substring(file.getOriginalFilename().lastIndexOf(".") + 1);
   }

   @SneakyThrows
   private void saveImage(final InputStream inputStream, final String fileName) {
      minioClient.putObject(PutObjectArgs.builder()
              .stream(inputStream, inputStream.available(), -1)
              .bucket(minioProperties.getBucket())
              .object(fileName)
              .build());
   }
}
