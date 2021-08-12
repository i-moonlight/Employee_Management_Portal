import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Token } from '../../models/token';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AUTH_API_URL } from '../../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Constants } from '../../common/constants';

export const ACCESS_TOKEN_KEY = 'access_token';

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
}
