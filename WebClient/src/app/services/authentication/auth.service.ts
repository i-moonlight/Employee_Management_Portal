import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Token } from '../../view-models/token';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AUTH_API_URL } from '../../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { map, tap } from 'rxjs/operators';
import { ResponseCode, ResponseModel } from '../../view-models/responseModel';
import { User } from '../../view-models/user';
import { Constants } from '../../common/constants';

export const ACCESS_TOKEN_KEY = 'access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  @Inject(AUTH_API_URL)
  private http: HttpClient;
  private jwtHelper: JwtHelperService;
  private router: Router;

  constructor(http: HttpClient, jwtHelper: JwtHelperService, router: Router) {
    this.http = http;
    this.jwtHelper = jwtHelper;
    this.router = router;
  };

  /*
  This method refers to the authorization server with mail and password.
  And in case of successful authentication, writes the token to the storage.
  */
  toLogin(email: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${Constants.AUTH_URL} login`, {
      email, password
    })
      .pipe(tap(token => localStorage.setItem(ACCESS_TOKEN_KEY, token.accessToken)))
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

  toUserAuthentication(userName, password) {
    let data = "username=" + userName + "&password=" + password + "&grant_type=password";
    let reqHeader = new HttpHeaders({'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True'});
    return this.http.post(Constants.AUTH_URL, data, {headers: reqHeader});
  }

  public toRegistration(fullName: string, email: string, password: string) {
    const body = {
      FullName: fullName,
      Email: email,
      Password: password
    }
    return this.http.post(Constants.AUTH_URL + 'RegisterUser', body);
  }

  public getUserList() {
    let userInfo = JSON.parse(localStorage.getItem(Constants.USER_KEY));
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${userInfo?.token}`
    });

    return this.http.get<ResponseModel>(Constants.AUTH_URL + 'GetAllUsers', {headers: headers}).pipe(map(res => {
      let userList = new Array<User>();
      if (res.responseCode == ResponseCode.OK) {
        if (res.dateSet) {
          res.dateSet.map((x: User) => {
            userList.push(new User(x.userId, x.fullName, x.email, x.userName, x.roles));
          })
        }
      }
      return userList;
    }));
  }
}
