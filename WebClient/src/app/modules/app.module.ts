import { ToastrModule } from 'ngx-toastr';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppComponent } from '../app.comp';
import { AppRoutingModule } from './app-routing.module';

import { AuthComponent } from '@auth/auth.component';
import { AuthGuard } from '@guards/auth.guard';
import { AuthInterceptor } from '@guards/auth.interceptor';
import { AuthService } from '@services/authentication/auth.service';
import { DepartmentComponent } from '../components/department/dep.comp';
import { DepartmentListComponent } from '../components/department/dep-list/dep-list.comp';
import { DepartmentModalComponent} from '../components/department/dep-modal/dep-modal.comp';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { EmployeeListComponent } from '../components/employee/emp-list/emp-list.comp';
import { EmployeeModalComponent } from '../components/employee/emp-modal/emp-modal.comp';
import { ForgotPasswordComponent } from '@auth/forgot-password/forgot-password.component';
import { LoginComponent } from '@auth/login/login.component';
import { ManagerComponent } from '../components/manager/manager.component';
import { RegisterComponent } from '@auth/registration/register.component';
import { SharedService } from '@services/shared/shared.service';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    ForgotPasswordComponent,
    LoginComponent,
    ManagerComponent,
    RegisterComponent,
  ],

  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    ToastrModule.forRoot(),
  ],

  providers: [
    AuthGuard,
    AuthService,
    SharedService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],

  bootstrap: [AppComponent]
})
export class AppModule {}
