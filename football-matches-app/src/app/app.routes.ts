import { Routes } from '@angular/router';
import { MatchListComponent } from './matches/controllers/matchcomponents/match-list.component';
import { LoginComponent } from './matches/controllers/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './matches/controllers/register/register.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'matches', component: MatchListComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },// default route
  { path: '**', redirectTo: '/login' } // Catch-all route for undefined routes
];