import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { DepartmentComponent } from '../components/department/dep.comp';
import { AuthComponent } from '../components/authentication/auth.component';
import { RegisterComponent } from '../components/authentication/registration/register.component';
import { LoginComponent } from '../components/authentication/login/login.component';
import { AuthGuard } from '../guards/auth.guard';

const routes: Routes = [
  {path: '', redirectTo: '/login', pathMatch: 'full'},
  {path: 'employee', component: EmployeeComponent, canActivate: [AuthGuard]},
  {path: 'department', component: DepartmentComponent},
  {path: 'auth', component: AuthComponent},
  {path: 'login', component: AuthComponent, children: [{path: '', component: LoginComponent}]},
  {path: 'registration', component: AuthComponent, children: [{path: '', component: RegisterComponent}]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
