import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  constructor(private formBuilder: FormBuilder, private authService: AuthService) {};
  
  // TODO: Bind this event handler to the login form as well
  onSubmit(): void {
    if (this.loginForm.valid) {
      const formValues = this.loginForm.value;

      this.authService.login(
        formValues.username!,
        formValues.password!
      ).subscribe((token: string) => {
        // TODO: Might need to change the BE a little bit so that it returns the token only on a successful response
        // TODO: As a nice to have - introduce a separate error handling mechanism here as well
        // TODO: Redirect to the cities list view after a success
        localStorage.setItem("cityAppToken", token);
      });
    }
  }
}
