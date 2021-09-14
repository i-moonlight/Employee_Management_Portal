import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/authentication/auth.service';
import { Account } from '../../../models/account.model';
import { ToastrService } from 'ngx-toastr';
import { Response } from '../../../models/response.model';
import { Router } from '@angular/router';
import { Login } from '../../../models/login.model';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import Validation from 'src/app/utils/validation';
import { Subject } from 'rxjs';
import { autoSave, autoSaveClear } from '../../../utils/auto-save';

const key = 'save1';
const keyS = new Subject<string>();

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  private readonly usernamePattern = /^[\S][\w\d]{6,16}$/;
  private readonly passwordPattern = /^((?!.*[\s])(?=.*[!@#$%^&*])(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{12,25})$/;
  private readonly emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/;

  @autoSave(keyS)
  public registerForm = new FormGroup({
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
      ]),
      confirmPassword: new FormControl('', [
        Validators.required
      ])
    },
    {
      // Check matching password.
      validators: [Validation.match('password', 'confirmPassword')]
    });

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
      ]),
      validators: [Validation.match('password', 'confirmPassword')]
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

  ngOnInit() {
    setTimeout(() => keyS.next('save1'), 1000);
  }

  onSubmit(registerForm: FormGroup) {
    autoSaveClear(key);

    let username = registerForm.value.userName;
    let email = registerForm.value.email;
    let password = registerForm.value.password;

    const account: Account = {
      UserName: username,
      Email:    email,
      Password: password,
    };

    this.authService.registerUser(account).subscribe((res: Response) => {
      if (res.DateSet == null) {
        this.toastr.success('Registration Successful', null, { timeOut: 8000 });
        this.authenticationUser(account);
      } else {
        this.toastr.error('Registration Failed', null, { timeOut: 8000 });
        console.warn(res.ResponseMessage);
      }
    });
  }

  onCancel() {
    autoSaveClear(key);
    if (this.registerForm != null) this.registerForm.reset();
  }

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

  public get username(): AbstractControl {
    return this.registerForm.controls.userName;
  }

  public get email(): AbstractControl {
    return this.registerForm.controls.email;
  }

  public get password(): AbstractControl {
    return this.registerForm.controls.password;
  }

  public get confirmPassword(): AbstractControl {
    return this.registerForm.controls.confirmPassword;
  }
}
