package com.github.webapi.domain.model;

import lombok.Data;

import java.time.LocalDateTime;

@Data
public class Employee {
    private Long id;
    private String fio;
    private String department;
    private String photoFileName;
    private LocalDateTime created;
}
