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
import { Cities } from '../types/cities';
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

  getAllCities(pageNumber: number, pageSize: number): Observable<Cities> {
    const params = {
      pageNumber: pageNumber,
      pageSize: pageSize,
    };

    return this.http
      .get<Cities>(`${environment.apiUrl}/Cities`, {
        params: params,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.message);
          return throwError(() => error);
        })
      );
  }

  searchCities(
    pageNumber: number,
    pageSize: number,
    name: string
  ): Observable<Cities> {
    const params = {
      name: name,
      pageNumber: pageNumber,
      pageSize: pageSize,
    };

    return this.http
      .get<Cities>(`${environment.apiUrl}/Cities/search`, {
        params: params,
      })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.message);
          return throwError(() => error);
        })
      );
  }

  editCity(city: City) {
    return this.http
      .put<HttpResponse<any>>(`${environment.apiUrl}/Cities/${city.id}`, city)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.logger.error(error.message);
          return throwError(() => error);
        })
      );
  }
}
