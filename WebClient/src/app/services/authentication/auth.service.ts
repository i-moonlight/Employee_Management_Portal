import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Token } from '../../models/token';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AUTH_API_URL } from '../../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Account } from '../../models/account.model';
import { Login } from '../../models/login.model';
import { Response } from '../../models/response.model';
import { Constants } from '../../common/constants';
import { Dto } from '@models/dto.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  @Inject(AUTH_API_URL)
  private http: HttpClient;
  private apiUrl: string;
  private jwtHelper: JwtHelperService;
  private router: Router;

  constructor(http: HttpClient, apiUrl: string, jwtHelper: JwtHelperService, router: Router) {
    this.http = http;
    this.apiUrl = apiUrl;
    this.jwtHelper = jwtHelper;
    this.router = router;
  }

  /*
  This method refers to the authorization server with mail and password.
  And in case of successful authentication, writes the token to the storage.
  */
  toLogin(email: string, password: string): Observable<Token> {
    return this.http
      .post<Token>(`${this.apiUrl} api/auth/login`, {email, password})
      .pipe(tap(token => localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)));
  }

  private readonly AUTH_URL: string = 'https://localhost:4021/api/auth/'

  constructor(private http: HttpClient) {}

  // Provides an access token from the authentication server.
  public getLoginToken(loginForm: Login): Observable<Response> {
    return this.http.post<Response>(this.AUTH_URL + 'SignIn', loginForm);
  };

  // This method reads the token and, if it exists, checks if it has expired.
  isAuthenticated(): boolean {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  // This method removes the token from the storage and redirects to the home page.
  toLogout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']).then(() => window.location.reload());
  }

  public toAuthentication(email: string, password: string) {
    const body = {
      Email: email,
      Password: password
    };
    return this.http.post('https://localhost:4021/api/auth/', body);
  }

  toUserAuthentication(userName, password) {
    const data = 'username=' + userName + '&password=' + password + '&grant_type=password';
    const reqHeader = new HttpHeaders({
      'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True'
    });
    return this.http.post(Constants.AUTH_URL, data, {headers: reqHeader});
  }

  public toRegistration(fullName: string, email: string, password: string) {
    const body = {
      FullName: fullName,
      Email: email,
      Password: password
    };
    return this.http.post('https://localhost:4021/api/auth/RegisterUser', body);
  }

  // Method of user authentication.
  public toUserAuthentication(loginForm: Login) {
    return this.http.post(Constants.AUTH_URL + 'SignIn', loginForm);
  }

  // Method of user registration.
  public registerUser(accountForm: Account): Observable<Response> {
    return this.http.post<Response>(this.AUTH_URL + 'RegisterUser', accountForm);
  }

  /**
   * Method of user forgot registration password.
   *
   * @return An `Observable` of the response, with the response body as a result execution ForgotPassword api method.
   */
  public sendForgotPasswordEmail(body: Dto): Observable<Response> {
    const headers = new HttpHeaders({
      'ChangePasswordUrl': 'url'
    });
    return this.http.post<Response>(this.AUTH_URL + 'ForgotPassword', body, {headers: headers});
  }
}
