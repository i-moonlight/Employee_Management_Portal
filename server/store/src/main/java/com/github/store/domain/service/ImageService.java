package com.github.store.domain.service;

import com.github.store.domain.model.Image;
import org.springframework.stereotype.Service;

@Service
public interface ImageService {
   String upload(Image image);
}
