import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/employee.component';
import { DepartmentComponent } from '../components/department/department.component';
import { AuthComponent } from '../components/authentication/auth.component';
import { RegisterComponent } from '../components/registration/register.component';

const routes: Routes = [
<<<<<<< HEAD
  { path: 'auth', component: AuthComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent }
=======
  {path: 'auth', component: AuthComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'employee', component: EmployeeComponent},
  {path: 'department', component: DepartmentComponent}
>>>>>>> 9d8de63 (feat: registration component)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {}
