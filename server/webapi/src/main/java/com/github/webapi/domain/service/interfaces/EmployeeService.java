package com.github.webapi.domain.service.interfaces;

import com.github.webapi.domain.model.Employee;

import java.util.List;

public interface EmployeeService {
    List<Employee> getEmployeeListFromRepository();
    Employee getEmployeeByIdFromRepository(long id);
    Long createEmployeeToRepository(Employee employee);
    void editEmployeeToRepository(Employee employee);
    void deleteEmployeeFromRepository(long id);
}
