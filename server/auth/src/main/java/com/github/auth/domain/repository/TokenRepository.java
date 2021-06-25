package com.github.auth.domain.repository;

public interface TokenRepository<K, V> {
    void saveToken(K key, V value);
    String getToken(K key);
    void deleteToken(K key);
}
