import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { ToastrService } from 'ngx-toastr';

import { AuthService } from '@services/authentication/auth.service';
import { Login } from '@models/login.model';
import { Pattern } from '@app/app.constants';
import { Response } from '@models/response.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router) {}

  public loginForm = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(7),
      Validators.pattern(Pattern.USERNAME_PATTERN)
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(Pattern.PASSWORD_PATTERN)
    ]),
  });

  ngOnInit(): void {}

  onSubmit(loginForm: FormGroup) {
    const username = loginForm.value.userName;
    const password = loginForm.value.password;

    const login: Login = {
      UserName: username,
      Password: password
    };

    // Passing data to a service
    this.authService.getLoginToken(login).subscribe((res: Response) => {
      if (res.DateSet != null) {
        console.warn(res.DateSet.Token);
        this.toastr.success('Login Successful', null, {timeOut: 8000});
        localStorage.setItem('token', res.DateSet.Token);

        // Redirect to employee URL
        setTimeout(() => {
          this.router.navigate(['/employee'], {skipLocationChange: true}).then(() =>
            window.location.reload());
        },
          2000);

      } else {
        this.toastr.error('Login Failed');
      }
    });
  }

  public get username() {
    return this.loginForm.controls.userName;
  }

  public get password() {
    return this.loginForm.controls.password;
  }
}
