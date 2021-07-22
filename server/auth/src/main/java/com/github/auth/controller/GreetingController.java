package com.github.auth.controller;

import io.swagger.v3.oas.annotations.tags.Tag;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/greet/")
@Tag(name = "Greeting Controller", description = "Greeting API")
public class GreetingController {

    @GetMapping("/hello")
    public ResponseEntity<String>  hello(@RequestParam(name = "name", defaultValue = "test") String name) {
        String res = String.format("Hello, %s", name);
        return new ResponseEntity<>(res , HttpStatus.OK);
    }

    @PreAuthorize("hasRole('USER')")
    @GetMapping("/user")
    public ResponseEntity<String> greetingUser() {
        return new ResponseEntity<>("Welcome, you have USER role", HttpStatus.OK);
    }

    @PreAuthorize("hasRole('ADMIN')")
    @GetMapping("/admin")
    public ResponseEntity<String> greetingAdmin() {
        return new ResponseEntity<>("Welcome, you have ADMIN role", HttpStatus.OK);
    }

    @PreAuthorize("hasRole('USER') or hasRole('ADMIN')")
    @GetMapping("/userOrAdmin")
    public ResponseEntity<String> greetingUserOrAdmin() {
        return new ResponseEntity<>("Welcome, you have USER and ADMIN role", HttpStatus.OK);
    }

    @PreAuthorize("hasRole('ANONYMOUS')")
    @GetMapping("/anonymous")
    public ResponseEntity<String> greetingAnonymous() {
        return new ResponseEntity<>("Welcome, you have USER and ADMIN role", HttpStatus.OK);
    }
}
