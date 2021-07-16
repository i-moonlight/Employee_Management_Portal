package com.github.auth.domain.object.password.dto;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.github.auth.domain.model.User;
import lombok.NoArgsConstructor;
import lombok.Setter;
import org.springframework.beans.factory.annotation.Value;

import java.util.Calendar;
import java.util.Date;

@NoArgsConstructor
@Setter
public class PasswordResetObject {
    private String token;
    private User user;
    private Date expiry;

    @Value("${spring.mail.username}")
    public String from;

    @JsonCreator(mode = JsonCreator.Mode.PROPERTIES)
    public PasswordResetObject(String token, User user, Date expiry, String from) {
        this.token = token;
        this.user = user;
        this.expiry = expiry;
        this.from = from;
    }

    public String getToken() {
        return token;
    }

    public User getUser() {
        return user;
    }

    public Date getExpiry() {
        Calendar calendar = Calendar.getInstance();
        calendar.setTimeInMillis(new Date().getTime());
        calendar.add(Calendar.MINUTE, 10);
        return new Date(calendar.getTime().getTime());
    }
}
