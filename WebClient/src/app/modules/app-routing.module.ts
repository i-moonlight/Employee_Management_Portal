import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
<<<<<<< HEAD
import { EmployeeComponent } from '../components/employee/employee.component';
import { DepartmentComponent } from '../components/department/department.component';
import { AuthComponent } from '../components/authentication/auth.component';
import { RegisterComponent } from '../components/authentication/registration/register.component';
import { LoginComponent } from '../components/authentication/login/login.component';
import { AuthGuard } from '../guards/auth.guard';

const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: AuthComponent, children: [{path: '', component: LoginComponent}]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'employee', component: EmployeeComponent, canActivate: [AuthGuard] },
  { path: 'registration', component: AuthComponent, children: [{path: '', component: RegisterComponent}] }
=======

import { AuthComponent } from '@auth/auth.component';
import { AuthGuard } from '@guards/auth.guard';
import { DepartmentComponent } from '../components/department/dep.comp';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { ForgotPasswordComponent } from '@auth/forgot-password/forgot-password.component';
import { LoginComponent } from '@auth/login/login.component';
import { RegisterComponent } from '@auth/registration/register.component';

const routes: Routes = [
  {path: '', redirectTo: 'auth/login', pathMatch: 'full'},
  {path: 'employee', component: EmployeeComponent, canActivate: [AuthGuard]},
  {path: 'department', component: DepartmentComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'auth/login', component: AuthComponent, children: [{path: '', component: LoginComponent}]},
  {path: 'auth/registration', component: AuthComponent, children: [{path: '', component: RegisterComponent}]},
  {path: 'forgot-password', component: ForgotPasswordComponent}
>>>>>>> c14fb54 (feat: forgot password component)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
