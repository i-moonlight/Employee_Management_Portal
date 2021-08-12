import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/authentication/auth.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { Constants } from '../../../common/constants';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isLoginError: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  onSubmit(userName, password) {
    this.authService.toUserAuthentication(userName, password).subscribe((data: any) => {
        localStorage.setItem('userToken', data.access_token);
        this.router.navigate([Constants.BASE_URL + 'employee']);
      },
      (err: HttpErrorResponse) => {
        this.isLoginError = true;
      });
  }
}
