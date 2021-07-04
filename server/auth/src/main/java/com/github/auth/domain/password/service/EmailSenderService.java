package com.github.auth.domain.password.service;

import com.github.auth.domain.service.EmailService;
import lombok.RequiredArgsConstructor;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class EmailSenderService implements EmailService {
    private final JavaMailSender mailSenderObj;

    @Override
    public void sendEmail(String email, String content, String subject) {
        mailSenderObj.send(mime -> {
            MimeMessageHelper mimeMsgHelperObj = new MimeMessageHelper(mime, true, "UTF-8");
            mimeMsgHelperObj.setTo(email);
            mimeMsgHelperObj.setText(content, true);
            mimeMsgHelperObj.setSubject(subject);
        });
    }
}
