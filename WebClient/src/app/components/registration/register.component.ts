import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../../services/authentication/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  private registerForm;

  constructor(private formBuilder: FormBuilder, private authService: AuthService) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      fullName: ['', [Validators.required]],
      email: ['', [Validators.email, Validators.required]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    const fullName = this.registerForm.controls.fullName.value;
    const email = this.registerForm.controls.email.value;
    const password = this.registerForm.controls.password.value;

    this.authService.toRegistration(fullName, email, password)
      .subscribe((data) => console.log('response', data),
        error => console.log('error', error)
      );
  }
}
