package com.github.webapi.controller;

import com.github.webapi.domain.model.Department;
import com.github.webapi.domain.service.interfaces.DepartmentService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins = "http://localhost:4200")
@RestController
@RequestMapping("/department")
public class DepartmentController {
   private final DepartmentService service;

   public DepartmentController(DepartmentService service) {
      this.service = service;
   }


   @PostMapping
   public Long createDepartment(@RequestBody Department department) {
      return service.createDepartmentToRepository(department);
   }

   @GetMapping("/{id}")
   public Department getDepartmentById(@PathVariable Long id) {
      return service.getDepartmentByIdFromRepository(id);
   }

   @PutMapping
   public void editEmployee(@RequestBody Department department) {
      service.editDepartmentToRepository(department);
   }

   @DeleteMapping("/{id}")
   public void deleteEmployee(@PathVariable long id) {
      service.deleteDepartmentFromRepository(id);
   }
}
