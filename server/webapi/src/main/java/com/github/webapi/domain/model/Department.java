package com.github.webapi.domain.model;

import lombok.Data;

import java.time.LocalDateTime;

@Data
public class Department {
   private Long id;
   private String name;
   private LocalDateTime created;
}
