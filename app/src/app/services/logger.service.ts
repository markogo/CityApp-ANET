import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LoggerService {
  constructor() {}

  info(message: string): void {
    console.log(message);
  }

  warn(message: string): void {
    console.warn(message);
  }

  error(message: string): void {
    console.error(message);
  }
}
