import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from '../app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeComponent } from '../components/employee/employee.component';
import { EmployeeListComponent } from '../components/employee/emp-list/emp-list.component';
import { EmployeeModalComponent } from '../components/employee/emp-modal/emp-modal.component';
import { DepartmentComponent } from '../components/department/department.component';
import { DepartmentListComponent } from '../components/department/dep-list/dep-list.component';
import { DepartmentModalComponent } from '../components/department/dep-modal/dep-modal.component';
import { SharedService } from '../services/shared/shared.service';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
  ],
  providers: [SharedService],
  bootstrap: [AppComponent],
})
export class AppModule {}
