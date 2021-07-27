package com.github.auth.dao;

import com.github.auth.domain.repository.TokenRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.stereotype.Repository;

@Repository
@RequiredArgsConstructor
public class TokenDao implements TokenRepository<String, String> {
    private final RedisTemplate<String, Object> template;

    /**
     * Working with Redis String Data
     */
    @Override
    public void saveToken(String key, String value) {
        template.opsForValue().set(key, value);
    }

    @Override
    public String getToken(String key) {
        return (String) template.opsForValue().get(key);
    }

    @Override
    public void deleteToken(String key) {
        template.opsForValue().getAndDelete(key);
    }
}
