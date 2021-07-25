package com.github.auth.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.github.auth.domain.object.account.dto.*;
import com.github.auth.domain.service.AccountService;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.*;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@AutoConfigureMockMvc
@RunWith(SpringRunner.class)
@SpringBootTest()
public class AuthControllerTests {

   @Autowired
   private MockMvc mockMvc;

   @MockBean
   private AccountService mockAccountService;

   @Autowired
   private ObjectMapper objectMapper;

   @Test
   public void it_signup_should_returns_response_json_success() throws Exception {
      AccountRequest request = AccountRequest.builder()
              .firstname("test")
              .lastname("test")
              .username("test")
              .email("test@gmail.com")
              .password("dmjti44h*8rgh4hf8")
              .passwordConfirm("dmjti44h*8rgh4hf8")
              .build();

      AuthResponse response = AuthResponse.builder()
              .status(HttpStatus.OK)
              .message("Test passed successfully")
              .accessToken(null)
              .refreshToken(null)
              .userInfoObject(null)
              .build();

      //Invoke service method -> AuthResponse
      when(mockAccountService.register(request)).thenReturn(response);

      //Invoke controller method with AccountRequest -> AuthResponse
      mockMvc.perform(post("/api/auth/signup")
                      .content(objectMapper.writeValueAsString(request)) //Input object -> json format
                      .contentType(MediaType.APPLICATION_JSON))
              //Compare return json assert value with class response value
              .andExpect(status().isOk())
              .andExpect(jsonPath("$.message").value(response.getMessage()))
              .andExpect(jsonPath("$.accessToken").value(response.getAccessToken()))
              .andExpect(jsonPath("$.refreshToken").value(response.getRefreshToken()))
              .andExpect(jsonPath("$.userInfoObject").value(response.getUserInfoObject()))
              .andDo(print());
   }

   @Test
   public void it_signin_should_returns_response_json_success() throws Exception {
      LoginRequest request = LoginRequest.builder()
              .username("test")
              .password("dmjti44h*8rgh4hf8")
              .build();

      AuthResponse response = AuthResponse.builder()
              .status(HttpStatus.OK)
              .message("Test passed successfully")
              .accessToken(null)
              .refreshToken(null)
              .userInfoObject(null)
              .build();

      when(mockAccountService.login(request)).thenReturn(response);

      mockMvc.perform(post("/api/auth/signin")
                      .content(objectMapper.writeValueAsString(request))
                      .contentType(MediaType.APPLICATION_JSON))
              .andExpect(status().isOk())
              .andExpect(jsonPath("$.message").value(response.getMessage()))
              .andExpect(jsonPath("$.accessToken").value(response.getAccessToken()))
              .andExpect(jsonPath("$.refreshToken").value(response.getRefreshToken()))
              .andExpect(jsonPath("$.userInfoObject").value(response.getUserInfoObject()))
              .andDo(print());
   }
}
