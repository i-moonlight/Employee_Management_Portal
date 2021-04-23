import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.comp';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeComponent } from './components/employee/emp.comp';
import { EmployeeListComponent } from './components/employee/emp-list/emp-list.comp';
import { EmployeeModalComponent } from './components/employee/emp-modal/emp-modal.comp';
import { DepartmentComponent } from './components/department/dep.comp';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    DepartmentComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
