import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

import { Account } from '@models/account.model'
import { AuthService } from '@services/authentication/auth.service';
import { autoSave, autoSaveClear } from '@utils/auto-save';
import { Login } from '@models/login.model';
import { Pattern } from '@app/app.constants';
import { Response } from '@models/response.model';
import Validation from '@utils/validation';

const Key = new Subject<string>();

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router) {}

  @autoSave(Key)
  public registerForm = new FormGroup({
      userName: new FormControl('', [
        Validators.required,
        Validators.minLength(7),
        Validators.pattern(Pattern.USERNAME_PATTERN)
      ]),
      email: new FormControl('', [
        Validators.email,
        Validators.required,
        Validators.pattern(Pattern.EMAIL_PATTERN)
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(Pattern.PASSWORD_PATTERN)
      ]),
      confirmPassword: new FormControl('', [
        Validators.required
      ])
    },
    {
      validators: [Validation.match('password', 'confirmPassword')]
    });

  ngOnInit() {
    setTimeout(() => Key.next('save1'), 1000);
  }

  onSubmit(registerForm: FormGroup) {
    let username = registerForm.value.userName;
    let email = registerForm.value.email;
    let password = registerForm.value.password;
    const account: Account = {
      UserName: username,
      Email: email,
      Password: password,
    }

    this.authService.registerUser(account).subscribe((res: Response) => {
      if (res.DateSet == null) {
        this.toastr.success('Registration Successful', null, {timeOut: 8000});
        this.authenticationUser(account);
      } else {
        this.toastr.error('Registration Failed', null, {timeOut: 8000});
        console.warn(res.ResponseMessage);
      }
    });
    autoSaveClear(Key);
  }

  onCancel() {
    autoSaveClear(Key);
    if (this.registerForm != null) this.registerForm.reset();
  }

  private authenticationUser(registeredAccount: Account) {
    const login: Login = {
      UserName: registeredAccount.UserName,
      Password: registeredAccount.Password
    }

    this.authService.getLoginToken(login).subscribe((res: Response) => {
      if (res.DateSet != null) {
        this.toastr.success('Login Successful');
        localStorage.setItem('token', res.DateSet.Token);
        this.router.navigate(['/employee']).then(() => window.location.reload());
      } else {
        this.toastr.error('Login Failed');
      }
    });
  }

  public get username(): AbstractControl {
    return this.registerForm.controls['userName'];
  }

  public get email(): AbstractControl {
    return this.registerForm.controls['email'];
  }

  public get password(): AbstractControl {
    return this.registerForm.controls['password'];
  }

  public get confirmPassword(): AbstractControl {
    return this.registerForm.controls['confirmPassword'];
  }
}
