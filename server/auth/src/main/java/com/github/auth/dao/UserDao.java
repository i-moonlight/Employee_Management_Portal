package com.github.auth.dao;

import com.github.auth.domain.model.User;
import com.github.auth.domain.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.util.*;

@Repository
@RequiredArgsConstructor
public class UserDao implements UserRepository {
    private final JdbcTemplate template;

    public Optional<User> findUserById(UUID userid) {
        try {
            return Optional.ofNullable(template.queryForObject(
                    "SELECT * FROM users WHERE id = ?", new UserRowMapper(), userid));
        } catch (EmptyResultDataAccessException e) {
            return Optional.empty();
        }
    }

    public Optional<User> findUserByName(String username) {
        try {
            return Optional.ofNullable(template.queryForObject(
                    "SELECT * FROM users WHERE username = ?", new UserRowMapper(), username));
        } catch (EmptyResultDataAccessException e) {
            return Optional.empty();
        }
    }

    public Optional<User> findUserByEmail(String email) {
        try {
            return Optional.ofNullable(template.queryForObject(
                    "SELECT * FROM users WHERE email = ?", new UserRowMapper(), email));
        } catch (EmptyResultDataAccessException e) {
            return Optional.empty();
        }
    }

    public void updateUserPassword(User user) {
        template.update("UPDATE users SET password = ? WHERE id = ?", user.getPassword(), user.getId());
    }

    public void saveUser(User user) {
        template.update("INSERT INTO users (id, firstname, lastname, username, email, password, role)" +
                        " VALUES (?, ?, ?, ?, ?, ?, ?)",
                user.getId(),
                user.getFirstname(), user.getLastname(), user.getUsername(),
                user.getEmail(),
                user.getPassword(),
                user.getRole().toString());
    }
}
