package com.github.auth.controller;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;
import static org.hamcrest.Matchers.containsString;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@AutoConfigureMockMvc
@RunWith(SpringRunner.class)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class GreetingControllerTests {

   @Autowired
   private MockMvc mockMvc;

   @Autowired
   private TestRestTemplate template;
   
   @Test
   public void it_hello_should_returns_json_type_response() throws Exception {
      mockMvc
              .perform(get("/api/greet/hello")
              .accept(MediaType.parseMediaType("application/json")))
              .andDo(print())
              .andExpect(status().isOk())
              .andExpect(content().contentType(MediaType.APPLICATION_JSON));
   }

   @Test
   public void it_hello_should_return_default_message() throws Exception {
      mockMvc
              .perform(get("/api/greet/hello").param("username", ""))
              .andDo(print())
              .andExpect(content().string(containsString("Hello, test")));
   }

   @Test
   public void it_hello_should_return_message_with_name() throws Exception {
      mockMvc
              .perform(get("/api/greet/hello?name=Dan"))
              .andExpect(content().string("Hello, Dan"));
   }

   @Test
   public void it_hello_should_return_default_message_ok() {
      ResponseEntity<String> res = template.getForEntity("/api/greet/hello", String.class);
      assertThat(res.getBody()).isEqualTo("Hello, test");
   }
}
