package com.github.webapi.domain.interfaces;

import com.github.webapi.domain.model.Department;

public interface DepartmentRepository {
   Long createDepartment(Department department);
   Department getDepartmentById(long id);
   void editDepartment(Department department);
   void deleteDepartment(long id);
}
