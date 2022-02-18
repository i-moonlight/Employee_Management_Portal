import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  public loginForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  })

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log("on submit")
    let email = this.loginForm.controls["email"].value;
    let password = this.loginForm.controls["password"].value;
  }
}
