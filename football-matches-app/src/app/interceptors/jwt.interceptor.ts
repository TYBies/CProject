import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../matches/services/auth.service';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if user is logged in and request is to the api url
    const currentUser = this.authService.currentUserValue;
    const isLoggedIn = currentUser && currentUser.token;
    const isApiUrl = request.url.startsWith('http://localhost:5259'); 

    //request is immutable, clone request and add new headers(Bearers Token)
    if (isLoggedIn && isApiUrl) {
      console.log('THIS IS USERTOKEN IN INTERCEPTOR',currentUser.token);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      });
    }

    return next.handle(request);
  }
}