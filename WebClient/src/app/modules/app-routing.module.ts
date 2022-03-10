import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
