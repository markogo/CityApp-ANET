import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpResponse,
} from '@angular/common/http';
import { LoggerService } from './logger.service';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetCitiesDTO } from '../types/getCities';
import { City } from '../types/city';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  constructor(
    private logger: LoggerService,
    private http: HttpClient,
    private authService: AuthService
  ) {}

  getAllCities(pageNumber: number, pageSize: number): Observable<GetCitiesDTO> {
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.authService.userProfile.value.jwt}`)
      .set('Content-Type', 'application/json');

    const params = {
      pageNumber: pageNumber,
      pageSize: pageSize,
    };

    return this.http
      .get<GetCitiesDTO>(`${environment.apiUrl}/Cities`, {
        headers: headers,
        params: params,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.error);
          return throwError(() => error);
        })
      );
  }

  searchCities(
    pageNumber: number,
    pageSize: number,
    name: string
  ): Observable<GetCitiesDTO> {
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.authService.userProfile.value.jwt}`)
      .set('Content-Type', 'application/json');

    const params = {
      name: name,
      pageNumber: pageNumber,
      pageSize: pageSize,
    };

    return this.http
      .get<GetCitiesDTO>(`${environment.apiUrl}/Cities/search`, {
        headers: headers,
        params: params,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.error);
          return throwError(() => error);
        })
      );
  }

  editCity(city: City) {
    const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${this.authService.userProfile.value.jwt}`)
      .set('Content-Type', 'application/json');

    return this.http
      .put<HttpResponse<any>>(`${environment.apiUrl}/Cities/${city.id}`, city, {
        headers: headers,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.error);
          return throwError(() => error);
        })
      );
  }
}
