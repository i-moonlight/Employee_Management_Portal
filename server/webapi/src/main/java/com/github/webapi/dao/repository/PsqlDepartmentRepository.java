package com.github.webapi.dao.repository;

import com.github.webapi.dao.mapper.DepartmentRowMapper;
import com.github.webapi.domain.interfaces.DepartmentRepository;
import com.github.webapi.domain.model.Department;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.stereotype.Repository;

@Repository
public class PsqlDepartmentRepository implements DepartmentRepository {
   private final NamedParameterJdbcTemplate template;

   public PsqlDepartmentRepository(NamedParameterJdbcTemplate template) {
      this.template = template;
   }

   @Override
   public Long createDepartment(Department department) {
      String sql = "INSERT INTO department (name) VALUES (:name) RETURNING 1";
      SqlParameterSource parameterSource = new MapSqlParameterSource()
          .addValue("name", department.getName());
      return template.queryForObject(sql, parameterSource, Long.class);
   }

   @Override
   public Department getDepartmentById(long id) {
      String sql = "SELECT * FROM department WHERE department.id = :id";
      SqlParameterSource parameterSource = new MapSqlParameterSource("id", id);
      return template.queryForObject(sql, parameterSource, new DepartmentRowMapper());
   }

   @Override
   public void editDepartment(Department department) {
      String sql = "UPDATE department SET name = :name WHERE id = :id";
      SqlParameterSource parameterSource = new MapSqlParameterSource()
          .addValue("name", department.getName());
      template.update(sql, parameterSource);

   }

   @Override
   public void deleteDepartment(long id) {
      String sql = "DELETE FROM department WHERE id = :id";
      SqlParameterSource parameterSource = new MapSqlParameterSource("id", id);
      template.update(sql, parameterSource);
   }
}
