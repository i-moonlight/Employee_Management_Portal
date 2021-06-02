package com.github.webapi.domain.service;

import com.github.webapi.domain.interfaces.DepartmentRepository;
import com.github.webapi.domain.model.Department;
import com.github.webapi.domain.service.interfaces.DepartmentService;
import org.springframework.stereotype.Service;

@Service
public class DaoDepartmentService implements DepartmentService {
   private final DepartmentRepository repository;

   public DaoDepartmentService(DepartmentRepository repository) {
      this.repository = repository;
   }

   @Override
   public Department getDepartmentByIdFromRepository(long id) {
      return repository.getDepartmentById(id);
   }

   @Override
   public Long createDepartmentToRepository(Department department) {
      return repository.createDepartment(department);
   }

   @Override
   public void editDepartmentToRepository(Department department) {
      repository.editDepartment(department);
   }

   @Override
   public void deleteDepartmentFromRepository(long id) {
      repository.deleteDepartment(id);
   }
}
