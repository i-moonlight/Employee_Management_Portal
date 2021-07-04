package com.github.auth.domain.password.dto;

import com.github.auth.domain.model.User;
import lombok.Data;
import org.springframework.beans.factory.annotation.Value;

import java.util.Calendar;
import java.util.Date;

@Data
public class PasswordResetToken {
    private Long id;
    private String token;
    private User user;
    private Date expiryDate;

    @Value("${spring.mail.username}")
    private String from;

    public Date getExpiryDate() {
        return expiryDate;
    }

    public void setExpiryDate(Date expiryDate) {
        this.expiryDate = expiryDate;
    }

    public void setExpiryDate(int minutes){
        Calendar now = Calendar.getInstance();
        now.add(Calendar.MINUTE, minutes);
        this.expiryDate = now.getTime();
    }

    public boolean isExpired() {
        return new Date().after(this.expiryDate);
    }
}
