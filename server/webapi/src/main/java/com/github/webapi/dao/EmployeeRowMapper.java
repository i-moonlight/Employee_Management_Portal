package com.github.webapi.dao;

import com.github.webapi.domain.model.Employee;
import org.jetbrains.annotations.NotNull;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.SQLException;

public class EmployeeRowMapper implements RowMapper<Employee> {
   @Override
   public Employee mapRow(@NotNull ResultSet rs, int rowNum) throws SQLException {
      Employee employee = new Employee();
      employee.setId(rs.getLong("id"));
      employee.setFio(rs.getString("fio"));
      employee.setDepartment(rs.getString("department"));
      employee.setPhotoFileName(rs.getString("photoFileName"));
      employee.setCreated(rs.getTimestamp("created").toLocalDateTime());
      return employee;
   }
}
