package com.github.webapi.domain.service;

import com.github.webapi.domain.model.Employee;
import com.github.webapi.domain.interfaces.EmployeeRepository;

import com.github.webapi.domain.service.interfaces.EmployeeService;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class DaoEmployeeService implements EmployeeService {
    private final EmployeeRepository repository;

    public DaoEmployeeService(EmployeeRepository repository) {
        this.repository = repository;
    }

    @Override
    public List<Employee> getEmployeeListFromRepository() {
        return repository.getEmployeeList();
    }

    @Override
    public Employee getEmployeeByIdFromRepository(long id) {
        return repository.getEmployeeById(id);
    }

    @Override
    public Long createEmployeeToRepository(Employee employee) {
        return repository.createEmployee(employee);
    }

    @Override
    public void editEmployeeToRepository(Employee employee) {
        repository.editEmployee(employee);
    }

    @Override
    public void deleteEmployeeFromRepository(long id) {
        repository.deleteEmployee(id);
    }
}
