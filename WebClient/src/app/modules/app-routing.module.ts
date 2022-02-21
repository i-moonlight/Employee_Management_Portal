import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { DepartmentComponent } from '../components/department/dep.comp';
import { AuthComponent } from '../components/authentication/auth.component';
import { RegisterComponent } from '../components/registration/register.component';
import { LoginComponent } from '../components/authentication/login/login.component';

const routes: Routes = [
  {path: 'employee', component: EmployeeComponent},
  {path: 'department', component: DepartmentComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'login', component: AuthComponent, children: [{path: '', component: LoginComponent}]},
  {path: '', redirectTo: '/login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
