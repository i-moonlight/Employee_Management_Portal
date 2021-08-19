import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
