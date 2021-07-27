package com.github.auth.dao;

import com.github.auth.domain.model.*;
import org.springframework.jdbc.core.RowMapper;

import java.sql.*;
import java.util.UUID;

public class UserRowMapper implements RowMapper<User> {
    @Override
    public User mapRow(ResultSet rs, int rowNum) throws SQLException {
        return User.builder()
                .id(UUID.fromString(rs.getString("id")))
                .firstname(rs.getString("firstname"))
                .lastname(rs.getString("lastname"))
                .username(rs.getString("username"))
                .email(rs.getString("email"))
                .password(rs.getString("password"))
                .role(Role.valueOf(rs.getString("role")))
                .build();
    }
}
