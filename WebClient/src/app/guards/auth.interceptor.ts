import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.headers.get('No-Auth') == "True")
      return next.handle(req.clone());

    if (localStorage.getItem('token') != null) {
      const clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'))
      });
      return next.handle(clonedReq).pipe(tap(
        succ => {},
        err => {
          if (err.status == 401) {
            localStorage.removeItem('token');
            this.router.navigate(['/login']).then(() => window.location.reload());
          }
        })
      );
    } else
      this.router.navigate(['/login']).then(() => window.location.reload());
  }
}
