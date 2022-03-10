import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Account } from '@models/account.model';
import { Login } from '@models/login.model';
import { Response } from '@models/response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly AUTH_URL: string = 'https://localhost:4021/api/auth/'

  constructor(private http: HttpClient) {}

  // Provides an access token from the authentication server.
  public getLoginToken(loginForm: Login): Observable<Response> {
    return this.http.post<Response>(this.AUTH_URL + 'SignIn', loginForm);
  };

  // Method of user registration.
  public registerUser(accountForm: Account): Observable<Response> {
    return this.http.post<Response>(this.AUTH_URL + 'RegisterUser', accountForm);
  }

  /**
   * Method of user forgot registration password.
   *
   * @return An `Observable` of the response, with the response body as a result execution RememberPassword method.
   */
  public forgotPassword(email: Account): Observable<Response> {
    return this.http.post<Response>(this.AUTH_URL + 'ForgotPassword', email);
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
