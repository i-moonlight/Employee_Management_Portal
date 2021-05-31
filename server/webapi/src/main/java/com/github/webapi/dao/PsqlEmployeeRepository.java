package com.github.webapi.dao;

import com.github.webapi.domain.model.Employee;
import com.github.webapi.domain.interfaces.EmployeeRepository;
import org.jetbrains.annotations.NotNull;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.core.namedparam.SqlParameterSource;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class PsqlEmployeeRepository implements EmployeeRepository {
    private final NamedParameterJdbcTemplate template;

    public PsqlEmployeeRepository(NamedParameterJdbcTemplate template) {
        this.template = template;
    }

    public Long createEmployee(@NotNull Employee employee) {
        String sql = "INSERT INTO employee (fio, department, photoFileName) "
                + "VALUES (:fio, :department, :photoFileName)" +
                " RETURNING 3";
        SqlParameterSource parameterSource = new MapSqlParameterSource()
                .addValue("fio", employee.getFio())
                .addValue("department", employee.getDepartment())
                .addValue("photoFileName", employee.getPhotoFileName());
        return template.queryForObject(sql, parameterSource, Long.class);
    }

    public List<Employee> getEmployeeList() {
        String sql = "SELECT * FROM employee";
        return template.query(sql, new EmployeeRowMapper());
    }

    public Employee getEmployeeById(long id) {
        String sql = "SELECT * FROM employee WHERE employee.id = :id";
        SqlParameterSource parameterSource = new MapSqlParameterSource("id", id);
        return template.queryForObject(sql, parameterSource, new EmployeeRowMapper());
    }

    public void editEmployee(@NotNull Employee employee) {
        String sql = "UPDATE employee" +
                " SET fio = :fio, department = :department, photoFileName = :photoFileName" +
                " WHERE id = :id";
        SqlParameterSource parameterSource = new MapSqlParameterSource()
                .addValue("id", employee.getId())
                .addValue("fio", employee.getFio())
                .addValue("department", employee.getDepartment())
                .addValue("photoFileName", employee.getPhotoFileName());
        template.update(sql, parameterSource);
    }

    public void deleteEmployee(long id) {
        String sql = "DELETE FROM employee WHERE id = :id";
        SqlParameterSource parameterSource = new MapSqlParameterSource("id", id);
        template.update(sql, parameterSource);
    }
}
