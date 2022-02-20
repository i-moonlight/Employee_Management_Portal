import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authentication/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  public authForm;

  constructor(private formBuilder: FormBuilder, private sharedService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit() {
    console.log('on submit')
    let email = this.authForm.controls['email'].value;
    let password = this.authForm.controls['password'].value;

    this.sharedService.toAuthentication(email, password).subscribe((response: any) => {
      if (response.ResponseCode == 1) {
        localStorage.setItem('userInfo', response.dateSet);
        this.router.navigate(["/employee"]);
      }
        console.log("response", response);
      },
      error => {
        console.log("error", error)
      })
  }
}
