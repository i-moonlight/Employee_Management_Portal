package com.github.auth.domain.repository;

import com.github.auth.domain.model.User;

import java.util.Optional;

public interface AuthRepository {
    Optional<User> getUserByName(String username);
    void saveUser(User user);
}
