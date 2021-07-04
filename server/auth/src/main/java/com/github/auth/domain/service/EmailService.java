package com.github.auth.domain.service;

public interface EmailService {
   void sendEmail(String email, String content, String subject);
}