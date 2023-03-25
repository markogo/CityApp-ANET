import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Authentication } from '../types/authentication';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private logger: LoggerService,
    private http: HttpClient,
    private router: Router
  ) {}

  userProfile: BehaviorSubject<Authentication> =
    new BehaviorSubject<Authentication>({
      username: (
        JSON.parse(localStorage.getItem('cityAppAuth')!) as Authentication
      )?.username,
      jwt: (JSON.parse(localStorage.getItem('cityAppAuth')!) as Authentication)
        ?.jwt,
      role: (JSON.parse(localStorage.getItem('cityAppAuth')!) as Authentication)
        ?.role,
    });

  register(username: string, password: string): Observable<Authentication> {
    return this.http
      .post<Authentication>(`${environment.apiUrl}/Users/register`, {
        username,
        password,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.message);
          return throwError(() => error);
        })
      );
  }

  login(username: string, password: string): Observable<Authentication> {
    return this.http
      .post<Authentication>(`${environment.apiUrl}/Users/login`, {
        username,
        password,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.message);
          return throwError(() => error);
        })
      );
  }

  logout() {
    this.userProfile.next({ username: '', jwt: '', role: '' });
    localStorage.removeItem('cityAppAuth');
    this.router.navigate(['/login']);
  }

  setAuthentication(user: Authentication) {
    this.userProfile.next(user);
    localStorage.setItem('cityAppAuth', JSON.stringify(user));
  }
}
