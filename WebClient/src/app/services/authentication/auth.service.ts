import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AUTH_API_URL } from '../../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Account } from '../../models/account.model';
import { Constants } from '../../common/constants';
import { Login } from '../../models/login.model';
import { Response } from '../../models/response.model';

export const ACCESS_TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  @Inject(AUTH_API_URL)
  private jwtHelper: JwtHelperService;
  private router: Router;

  constructor(private http: HttpClient) {
    // this._isLoggedIn.next(!!token);
  }

  // Provides an access token from the authentication server.
  public getLoginToken(loginForm: Login): Observable<Response> {
    return this.http.post<Response>(Constants.AUTH_URL + 'SignIn', loginForm);
  };

  // This method reads the token and, if it exists, checks if it has expired.
  isAuthenticated(): boolean {
    let token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  };

  // This method removes the token from the storage and redirects to the home page.
  toLogout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']).then(() => window.location.reload());
  };

  public toAuthentication(email: string, password: string) {
    const body = {
      Email: email,
      Password: password
    }
    return this.http.post(Constants.AUTH_URL, body);
  }

  // Method of user authentication.
  public toUserAuthentication(loginForm: Login) {
    return this.http.post(Constants.AUTH_URL + 'SignIn', loginForm);
  }

  // Method of user registration.
  public registerUser(accountForm: Account): Observable<Response> {
    return this.http.post<Response>(Constants.AUTH_URL + 'RegisterUser', accountForm);
  }

  // public getUserList() {
  //   let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
  //   const headers = new HttpHeaders({
  //     'Authorization': `Bearer ${userInfo?.token}`
  //   });
  //
  //   return this.http.get<ResponseModel>(Constants.AUTH_URL + 'GetAllUsers', {headers: headers}).pipe(map(res => {
  //     let userList = new Array<User>();
  //     if (res.responseCode == ResponseCode.OK) {
  //       if (res.dateSet) {
  //         res.dateSet.map((x: User) => {
  //           userList.push(new User(x.userId, x.fullName, x.email, x.userName, x.roles));
  //         })
  //       }
  //     }
  //     return userList;
  //   }));
  // }
}
