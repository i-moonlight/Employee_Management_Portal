package com.github.webapi.domain.model;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class Image {
   private MultipartFile file;
}
