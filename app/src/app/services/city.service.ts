import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private logger: LoggerService, private http: HttpClient) {}

  // TODO: Add funcitonality for retrieving cities from the API
}
