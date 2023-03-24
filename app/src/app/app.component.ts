import { Component } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from './services/auth.service';
import { Authentication } from './types/authentication';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'CITY APP';
  userInfo?: Authentication;

  constructor(
    private authService: AuthService,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) {
    this.matIconRegistry.addSvgIcon(
      'proekspert_logo',
      this.domSanitizer.bypassSecurityTrustResourceUrl(
        'assets/icons/Proekspert_TurquoiseLogo.svg'
      )
    );
  }

  ngOnInit() {
    this.authService.userProfile.subscribe((data) => {
      this.userInfo = data;
    });
  }

  logout = () => {
    this.authService.logout();
  };

  onLogoClick = () => {
    window.location.href = 'https://proekspert.com';
  };
}
