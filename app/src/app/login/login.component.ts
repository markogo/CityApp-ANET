import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Authentication } from '../types/authentication';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  errorMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (this.loginForm.valid) {
      const formValues = this.loginForm.value;

      this.authService
        .login(formValues.username!, formValues.password!)
        .subscribe({
          next: (response: Authentication) => {
            this.authService.setAuthentication(response);
            this.router.navigate(['/cities']);
          },
          error: (error: HttpErrorResponse) => {
            this.errorMessage = error.error;
          },
        });
    }
  }
}
