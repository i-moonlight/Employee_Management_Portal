import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SharedService } from '../../services/shared/shared.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  public loginForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required]
  });

  private authForm;

  constructor(private formBuilder: FormBuilder, private sharedService: SharedService) {}

  ngOnInit(): void {
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    console.log('on submit');
    const email = this.loginForm.controls.email.value;
    const password = this.loginForm.controls.password.value;

    console.log('on submit');
    const email = this.authForm.controls.email.value;
    const password = this.authForm.controls.password.value;

    this.sharedService.toAuthentication(email, password).subscribe((data) => {
      console.log("response", data);
      },
        error => {
      console.log("error", error);
    });
  }
}
