import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CitiesListComponent } from './cities-list/cities-list.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';

// TODO: As a nice to have add a separate auth guard to the cities and login route to ensure that logged in users would be redirected accordingly
const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "cities", component: CitiesListComponent },
  { path: "", redirectTo: "cities", pathMatch: "full" },
  { path: "**", component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
