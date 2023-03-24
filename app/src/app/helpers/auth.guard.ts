import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate() {
    const userProfile = this.authService.userProfile.value;
    if (
      userProfile.jwt === '' &&
      userProfile.role === '' &&
      userProfile.username === ''
    ) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
