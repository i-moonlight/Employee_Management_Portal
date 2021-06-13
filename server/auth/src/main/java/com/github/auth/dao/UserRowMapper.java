package com.github.auth.dao;

import com.github.auth.domain.model.Role;
import com.github.auth.domain.model.User;
import org.jetbrains.annotations.NotNull;
import org.springframework.jdbc.core.RowMapper;

import java.sql.ResultSet;
import java.sql.SQLException;
public class UserRowMapper implements RowMapper<User> {
    @Override
    public User mapRow(@NotNull ResultSet rs, int rowNum) throws SQLException {
        return User.builder()
                //.id(rs.getInt("id"))
                .firstname(rs.getString("firstname"))
                .lastname(rs.getString("lastname"))
                .username(rs.getString("username"))
                .email(rs.getString("email"))
                .password(rs.getString("password"))
                .role(Role.valueOf(rs.getString("role")))
                .build();
    }
}
