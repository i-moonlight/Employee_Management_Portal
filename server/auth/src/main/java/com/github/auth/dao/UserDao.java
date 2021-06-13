package com.github.auth.dao;

import com.github.auth.domain.model.User;
import com.github.auth.domain.repository.AuthRepository;
import lombok.RequiredArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
@RequiredArgsConstructor
public class UserDao implements AuthRepository {
    private final JdbcTemplate template;

    public Optional<User> getUserByName(String username) {
        try {
            return Optional.ofNullable(template.queryForObject("SELECT * FROM users WHERE username = ?",
                    new UserRowMapper(), username));
        } catch (EmptyResultDataAccessException e) {
            return Optional.empty();
        }
    }

    public void saveUser(@NotNull User user) {
        template.update("INSERT INTO users(id, firstname, lastname, username, email, password, role)" +
                        " VALUES (?, ?, ?, ?, ?, ?, ?)",
                user.getId(),
                user.getFirstname(), user.getLastname(), user.getUsername(),
                user.getEmail(),
                user.getPassword(),
                user.getRole().toString());
    }
}
