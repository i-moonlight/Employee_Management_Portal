import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { DepartmentComponent } from '../components/department/dep.comp';
import { AuthComponent } from '../components/authentication/auth.component';

const routes: Routes = [
  {path: 'auth', component: AuthComponent},
  {path: 'employee', component: EmployeeComponent},
  {path: 'department', component: DepartmentComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {}
