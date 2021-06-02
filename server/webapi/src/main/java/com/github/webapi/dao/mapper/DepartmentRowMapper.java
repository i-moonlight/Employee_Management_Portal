package com.github.webapi.dao.mapper;

import com.github.webapi.domain.model.Department;
import org.jetbrains.annotations.NotNull;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.SQLException;

public class DepartmentRowMapper implements RowMapper<Department> {
   @Override
   public Department mapRow(@NotNull ResultSet rs, int rowNum) throws SQLException {
      Department department = new Department();
      department.setId(rs.getLong("id"));
      department.setName(rs.getString("name"));
      department.setCreated(rs.getTimestamp("created").toLocalDateTime());
      return department;
   }
}
