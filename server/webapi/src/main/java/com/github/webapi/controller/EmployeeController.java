package com.github.webapi.controller;

import com.github.webapi.domain.model.Employee;
import com.github.webapi.domain.service.interfaces.EmployeeService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/employee")
public class EmployeeController {
    private final EmployeeService service;

    public EmployeeController(EmployeeService service) {
        this.service = service;
    }

    @GetMapping
    public List<Employee> getEmployeeList() {
        return service.getEmployeeListFromRepository();
    }

    @PostMapping
    public Long createEmployee(@RequestBody Employee employee) {
        return service.createEmployeeToRepository(employee);
    }

    @GetMapping("/{id}")
    public Employee getEmployeeById(@PathVariable Long id){
        return service.getEmployeeByIdFromRepository(id);
    }

    @PutMapping
    public void editEmployee(@RequestBody Employee employee) {
        service.editEmployeeToRepository(employee);
    }

    @DeleteMapping("/{id}")
    public void deleteEmployee(@PathVariable long id) {
        service.deleteEmployeeFromRepository(id);
    }
}
