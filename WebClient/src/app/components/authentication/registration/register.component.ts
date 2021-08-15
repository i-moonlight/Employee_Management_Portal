import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../../services/authentication/auth.service';
import { Account } from '../../../models/account.model';
import { ToastrService } from 'ngx-toastr';
import { Response } from '../../../models/response.model';
import { Router } from '@angular/router';
import { Login } from '../../../models/login.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public account: Account;
  public emailPattern = '^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$';

  constructor(private authService: AuthService, private toastr: ToastrService, private router: Router) {}

  ngOnInit() {
    this.resetForm();
  }

  private resetForm(form?: NgForm) {
    if (form != null) form.reset();
    this.account = {
      UserName: '',
      Password: '',
      Email: '',
    };
  }

  onSubmit(form: NgForm) {
    this.account = {
      UserName: form.value.UserName,
      Password: form.value.Password,
      Email: form.value.Email,
    };
    this.authService.registerUser(this.account).subscribe((res: Response) => {
      if (res.DateSet == null) {
        this.toastr.success('Registration Successful');
        this.authenticationUser(this.account);
        this.resetForm(form);
      } else {
        this.toastr.error('Registration Failed');
        console.warn(res.ResponseMessage);
      }
    });
  }

  private authenticationUser(account) {
    const login: Login = {
      UserName: account.UserName,
      Password: account.Password
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
}
