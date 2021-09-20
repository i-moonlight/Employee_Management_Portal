import { ToastrModule } from 'ngx-toastr';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
<<<<<<< HEAD
import { AppComponent } from '../app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EmployeeComponent } from '../components/employee/employee.component';
import { EmployeeListComponent } from '../components/employee/emp-list/emp-list.component';
import { EmployeeModalComponent } from '../components/employee/emp-modal/emp-modal.component';
import { DepartmentComponent } from '../components/department/department.component';
import { DepartmentListComponent } from '../components/department/dep-list/dep-list.component';
import { DepartmentModalComponent} from '../components/department/dep-modal/dep-modal.component';
import { SharedService } from '../services/shared/shared.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from '../components/authentication/registration/register.component';
import { LoginComponent } from '../components/authentication/login/login.component';
import { AuthComponent } from '../components/authentication/auth.component';
import { AuthService } from '../services/authentication/auth.service';
import { AuthGuard } from '../guards/auth.guard';
import { AuthInterceptor } from '../guards/auth.interceptor';
=======
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
>>>>>>> c14fb54 (feat: forgot password component)

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent,
<<<<<<< HEAD
    RegisterComponent,
    RegisterComponent,
    LoginComponent
=======
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    ForgotPasswordComponent,
    LoginComponent,
    ManagerComponent,
    RegisterComponent,
>>>>>>> c14fb54 (feat: forgot password component)
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

    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem(ACCESS_TOKEN_KEY),
        allowedDomains:   ['localhost:9090'],
        disallowedRoutes: ['localhost:9090/api/auth']
      }
    })
  ],

  providers: [
    AuthGuard,
<<<<<<< HEAD
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}],
    bootstrap: [AppComponent]
=======
    AuthService,
    SharedService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],

  bootstrap: [AppComponent]
>>>>>>> c14fb54 (feat: forgot password component)
})

export class AppModule {}
