import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../types/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {}

  register(username: string, password: string): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/Users/register`, {
      username,
      password
    });
  }

  login(username: string, password: string): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/Users/login`, {
      username,
      password
    });
  }

  // TODO: Decied if this is necessary - seemed more appropriate to return user info (including roles) from a separate endpoint instead of decoding the token in the frontend
  getUserInfo(): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}/Users/whoami`);
  }
}
