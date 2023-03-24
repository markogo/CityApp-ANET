import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CitiesListComponent } from './cities-list/cities-list.component';
import { AuthGuard } from './helpers/auth.guard';
import { getIsLoggedIn } from './helpers/authentication';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cities', component: CitiesListComponent, canActivate: [AuthGuard] },
  {
    path: '',
    redirectTo: getIsLoggedIn() ? 'cities' : 'login',
    pathMatch: 'full',
  },
  { path: '**', redirectTo: getIsLoggedIn() ? 'cities' : 'login' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
