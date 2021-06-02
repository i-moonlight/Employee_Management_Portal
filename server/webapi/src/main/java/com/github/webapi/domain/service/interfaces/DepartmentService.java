package com.github.webapi.domain.service.interfaces;

import com.github.webapi.domain.model.Department;

import java.util.List;

public interface DepartmentService {
   Department getDepartmentByIdFromRepository(long id);
   Long createDepartmentToRepository(Department department);
   void editDepartmentToRepository(Department department);
   void deleteDepartmentFromRepository(long id);
}
