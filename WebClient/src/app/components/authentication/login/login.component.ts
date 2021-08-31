import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/authentication/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Login } from '../../../models/login.model';
import { Response } from '../../../models/response.model';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  // tslint:disable-next-line:typedef
  public get username() {
    return this.form.get('username').errors;
  }

  // tslint:disable-next-line:typedef
  public get password() {
    return this.form.get('password').errors;
  }
  public form: FormGroup;
  public usernamePattern = /^[a-z0-9_-]{6,16}$/;
  public passwordPattern = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=\\S+$).{8,}$/;

  static cannotContainSpace(control: AbstractControl): ValidationErrors | null {
    const password = control.value as string;
    if (password.indexOf(' ') >= 0) {
      return {
        cannotContainSpace: true
      };
    }
    return null;
  }

  static shouldBeUniqueUserName(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise<ValidationErrors | null>((resolve, _reject) => {
      return setTimeout(() => {
          console.log('ok');
          if ((control.value as string) !== 'mosh') {
            resolve(null);
          } else {
            return resolve({
              shouldBeUniqueUserName: true
            });
          }
        },
        2000);
    });
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username: ['',
        [Validators.required, Validators.minLength(6), Validators.maxLength(16)]],
      username: ['', Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(16),
        Validators.pattern(this.usernamePattern),
        LoginComponent.shouldBeUniqueUserName,
        LoginComponent.cannotContainSpace,
      ])],
      password: ['',
        [Validators.required, Validators.pattern(this.passwordPattern), LoginComponent.cannotContainSpace]],
    });
  }

  // tslint:disable-next-line:typedef
  onSubmit(userName, password) {
    const login: Login = {
      UserName: userName,
      Password: password,
    };
    this.authService.getLoginToken(login).subscribe((res: Response) => {
      if (res.DateSet != null) {
        this.toastr.success('Login Successful', null, {timeOut: 50000});
        localStorage.setItem('token', res.DateSet.Token);
        this.router.navigate(['/employee']).then(() => window.location.reload());
      } else {
        this.toastr.error('Login Failed');
      }
    });
  }
}
