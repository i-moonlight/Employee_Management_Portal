import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/employee.component';
import { DepartmentComponent } from '../components/department/department.component';
import { AuthComponent } from '../components/authentication/auth.component';
import { RegisterComponent } from '../components/authentication/registration/register.component';
import { LoginComponent } from '../components/authentication/login/login.component';

const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: AuthComponent, children: [{path: '', component: LoginComponent}]},
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {path: 'registration', component: AuthComponent, children: [{path: '', component: RegisterComponent}]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
