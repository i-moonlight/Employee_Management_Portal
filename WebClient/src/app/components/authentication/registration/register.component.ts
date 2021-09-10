import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/authentication/auth.service';
import { Account } from '../../../models/account.model';
import { ToastrService } from 'ngx-toastr';
import { Response } from '../../../models/response.model';
import { Router } from '@angular/router';
import { Login } from '../../../models/login.model';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;
  private readonly usernamePattern = /^[\S][\w\d]{6,16}$/;
  private readonly passwordPattern = /^((?!.*[\s])(?=.*[!@#$%^&*])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{12,25})$/;
  private readonly emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;

  constructor(private authService: AuthService, private formBuilder: FormBuilder,
              private toastr: ToastrService, private router: Router) {}

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      userName: new FormControl('', [
        Validators.required,
        Validators.minLength(7),
        Validators.pattern(this.usernamePattern)
      ]),
      email: new FormControl('', [
        Validators.email,
        Validators.required,
        Validators.pattern(this.emailPattern)
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(this.passwordPattern)
      ])
    });
  }

  // tslint:disable-next-line:typedef
  private resetForm(form?: NgForm) {
    if (form != null) { form.reset(); }
    this.account = {
      UserName: '',
      Password: '',
      Email: '',
    };
  }

  // tslint:disable-next-line:typedef
  onSubmit(form: NgForm) {
    this.account = {
      UserName: form.value.UserName,
      Password: form.value.Password,
      Email: form.value.Email,
    };

  onSubmit(registerForm: FormGroup) {
<<<<<<< HEAD
    const username = registerForm.controls['userName'].value;
    const email = registerForm.controls['email'].value;
    const password = registerForm.controls['password'].value;
=======
    let username = registerForm.value.userName;
    let email = registerForm.value.email;
    let password = registerForm.value.password;
>>>>>>> db7a5ce (refactor: authentication interceptor)

    const account: Account = {
      UserName: username,
      Email: email,
      Password: password,
    };

    this.authService.registerUser(account).subscribe((res: Response) => {
      if (res.DateSet == null) {
        this.toastr.success('Registration Successful', null, {timeOut: 8000});
        this.authenticationUser(account);
        // this.resetForm(registrForm);
      } else {
        this.toastr.error('Registration Failed', null, {timeOut: 8000});
        console.warn(res.ResponseMessage);
      }
    });
  }

  // tslint:disable-next-line:typedef
  private authenticationUser(account) {
    const login: Login = {
      UserName: account.UserName,
      Password: account.Password
    };

  private authenticationUser(registeredAccount: Account) {
    const login: Login = {
      UserName: registeredAccount.UserName,
      Password: registeredAccount.Password
    };

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

  public get username() {
<<<<<<< HEAD
    return this.registrationForm.controls.userName;
  }

  public get email() {
    return this.registrationForm.controls.email;
  }

  public get password() {
    return this.registrationForm.controls.password;
=======
    return this.registerForm.controls['userName'];
  }

  public get email() {
    return this.registerForm.controls['email'];
  }

  public get password() {
    return this.registerForm.controls['password'];
>>>>>>> db7a5ce (refactor: authentication interceptor)
  }
}
