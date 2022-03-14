import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/authentication/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from '../../../models/login.model';
import { Response } from '../../../models/response.model';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Pattern } from 'src/app/app.constants';

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
  })

  ngOnInit(): void {}

  // private static cannotContainSpace(control: AbstractControl): ValidationErrors | null {
  //   let password = control.value as string;
  //   if (password.indexOf(' ') >= 0) {
  //     return {
  //       cannotContainSpace: true
  //     };
  //   }
  //   return null;
  // }
  //
  // private static shouldBeUniqueUserName(control: AbstractControl): Promise<ValidationErrors | null> {
  //   return new Promise<ValidationErrors | null>((resolve, _reject) => {
  //     return setTimeout(() => {
  //         console.log('ok')
  //         if ((control.value as string) !== 'mosh') {
  //           resolve(null)
  //         } else {
  //           return resolve({
  //             shouldBeUniqueUserName: true
  //           });
  //         }
  //       },
  //       2000)
  //   });
  // }

  onSubmit(loginForm: FormGroup) {
    let username = loginForm.value.userName;
    let password = loginForm.value.password;

    const login: Login = {
      UserName: username,
      Password: password
    }

    this.authService.getLoginToken(login).subscribe((res: Response) => {
      if (res.DateSet != null) {
        console.warn(res.DateSet.Token);
        this.toastr.success('Login Successful', null, {timeOut: 8000});
        localStorage.setItem('token', res.DateSet.Token);
        this.router.navigate(['/employee']).then(() => window.location.reload());
      } else {
        this.toastr.error('Login Failed');
      }
    });
  }

  public get username() {
    return this.loginForm.controls['userName'];
  }

  public get password() {
    return this.loginForm.controls['password'];
  }
}