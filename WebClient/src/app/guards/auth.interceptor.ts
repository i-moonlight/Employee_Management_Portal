import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  // tslint:disable-next-line:typedef
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (req.url.includes('api')) {
      let newHeaders = req.headers.append('Accept', 'application/json');
      if (req.method === 'POST') {
        newHeaders = newHeaders.append('Content-Type', 'application/json');
      }
      return next.handle(req.clone({headers: newHeaders}));
    } else {
      return next.handle(req);
    }
  }
}
