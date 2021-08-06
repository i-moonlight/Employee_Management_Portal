import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/employee.component';
import { DepartmentComponent } from '../components/department/department.component';
import { AuthComponent } from '../components/authentication/auth.component';

const routes: Routes = [
  { path: 'auth', component: AuthComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'department', component: DepartmentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {}
