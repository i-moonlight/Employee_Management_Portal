import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (req.url.includes('api')) {
      let newHeaders = req.headers.append('Accept', 'application/json');
      if (req.method === 'POST') {
        newHeaders = newHeaders.append('Content-Type', 'application/json');
      }
      return next.handle(req.clone({headers: newHeaders}));
    } else
      return next.handle(req);
  }

  // intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  //   if (req.headers.get('No-Auth') == "True")
  //     return next.handle(req.clone());
  //
  //   if (localStorage.getItem('token') != null) {
  //     const clonedReq = req.clone({
  //       headers: req.headers.set('Authorization', 'Bearer' + localStorage.getItem('token')),
  //       withCredentials: true
  //     });
  //
  //     return next.handle(clonedReq).pipe(
  //       catchError(error => {
  //         if (error.status === 401 || error.status === 403) {
  //           localStorage.removeItem('token');
  //           this.router.navigate(['/login']).then(() => window.location.reload());
  //         }
  //         return throwError(error);
  //       })
  //     );
  //   } else
  //     this.router.navigate(['/login']).then(() => window.location.reload());
  // }
}
