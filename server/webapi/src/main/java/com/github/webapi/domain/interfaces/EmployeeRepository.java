package com.github.webapi.domain.interfaces;

import com.github.webapi.domain.model.Employee;

import java.util.List;

public interface EmployeeRepository {
    List<Employee> getEmployeeList();
    Long createEmployee(Employee employee);
    Employee getEmployeeById(long id);
    void editEmployee(Employee employee);
    void deleteEmployee(long id);
}
