package com.github.auth.dao;

import com.github.auth.domain.repository.TokenRepository;
import lombok.RequiredArgsConstructor;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.redis.core.*;
import org.springframework.stereotype.Repository;

@Repository
@RequiredArgsConstructor
public class TokenDao implements TokenRepository<String, String> {
    private final RedisTemplate<String, Object> template;

    /**
     * Работать с данными строкового типа Redis
     */
    @Override
    public void saveToken(@NotNull String key, String value) {
        template.opsForValue().set(key, value);
    }

    @Override
    public String getToken(@NotNull String key) {
        return (String) template.opsForValue().get(key);
    }

    @Override
    public void deleteToken(@NotNull String key) {
        template.opsForValue().getAndDelete(key);
    }
}
