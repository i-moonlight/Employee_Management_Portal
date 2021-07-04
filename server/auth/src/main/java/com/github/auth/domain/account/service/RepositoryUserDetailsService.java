package com.github.auth.domain.account.service;

import com.github.auth.domain.model.AuthUserDetails;
import com.github.auth.domain.model.User;
import com.github.auth.domain.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class RepositoryUserDetailsService implements UserDetailsService {
    @Autowired
    private UserRepository repository;

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        Optional<User> user = repository.findUserByName(username);
        return user.map(AuthUserDetails::new).orElseThrow(() ->
                new UsernameNotFoundException("user not found " + username));
    }
}