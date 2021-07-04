package com.github.auth.domain.repository;

import com.github.auth.domain.model.User;

import java.util.Optional;
import java.util.UUID;

public interface UserRepository {
    Optional<User> findUserById(UUID id);
    Optional<User> findUserByName(String username);
    Optional<User> findUserByEmail(String email);
    void saveUser(User user);
}
