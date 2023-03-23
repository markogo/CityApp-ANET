import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Authentication } from '../types/authentication';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });
  errorMessage: number | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(): void {
    if (this.registerForm.valid) {
      const formValues = this.registerForm.value;

      this.authService
        .register(formValues.username!, formValues.password!)
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
