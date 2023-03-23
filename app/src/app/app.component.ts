import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Authentication } from './types/authentication';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'CityApp';
  userInfo?: Authentication;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit() {
    this.authService.userProfile.subscribe((data) => {
      this.userInfo = data;
    });
  }

  logout = () => {
    this.authService.logout();
    this.router.navigate(['/login']);
  };
}
