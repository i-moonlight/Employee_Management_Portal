package com.github.auth.domain.object.password.service;

import com.github.auth.domain.service.EmailService;
import lombok.RequiredArgsConstructor;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class EmailSenderService implements EmailService {
    private final JavaMailSender mailSender;

    @Override
    public void sendEmail(String email, String content, String subject) {
        mailSender.send(mime -> {
            MimeMessageHelper msgHelper = new MimeMessageHelper(mime, true, "UTF-8");
            msgHelper.setTo(email);
            msgHelper.setText(content, true);
            msgHelper.setSubject(subject);
        });
    }
}
