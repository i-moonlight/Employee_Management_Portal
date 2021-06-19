package com.github.auth.domain.repository;

import com.github.auth.domain.model.User;

import java.util.Optional;
import java.util.UUID;

public interface AuthRepository {
    Optional<User> findUserById(UUID id);
    Optional<User> getUserByName(String username);
    void saveUser(User user);
}
