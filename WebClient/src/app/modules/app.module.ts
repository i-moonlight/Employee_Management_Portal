import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgProgressModule } from '@ngx-progressbar/core';
import { ToastrModule } from 'ngx-toastr';
import { AlertModule } from '@app/components/elements/alert/alert.module';
import { AppComponent } from '../app.comp';
import { AppRoutingModule } from './app-routing.module';
import { AuthComponent } from '@auth/auth.component';
import { AuthGuard } from '@guards/auth.guard';
import { AuthInterceptor } from '@guards/auth.interceptor';
import { AuthService } from '@services/authentication/auth.service';
import { ChangePasswordComponent } from '@auth/change-password/change-password.component';
import { DepartmentComponent } from '@components/department/dep.comp';
import { DepartmentListComponent } from '@components/department/dep-list/dep-list.comp';
import { DepartmentModalComponent} from '@components/department/dep-modal/dep-modal.comp';
import { EmployeeComponent } from '@components/employee/emp.comp';
import { EmployeeListComponent } from '@components/employee/emp-list/emp-list.comp';
import { EmployeeModalComponent } from '@components/employee/emp-modal/emp-modal.comp';
import { ForgotPasswordComponent } from '@auth/forgot-password/forgot-password.component';
import { HeaderComponent } from '@components/elements/header/header.component';
import { LoginComponent } from '@auth/login/login.component';
import { ManagerComponent } from '@components/manager/manager.component';
import { ProgressBarService } from '@services/progress-bar/progress-bar.service';
import { RegisterComponent } from '@auth/registration/register.component';
import { SharedService } from '@services/shared/shared.service';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    ChangePasswordComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    ForgotPasswordComponent,
    HeaderComponent,
    LoginComponent,
    ManagerComponent,
    RegisterComponent,
  ],
  imports: [
    AlertModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    ToastrModule.forRoot(),
    NgProgressModule,
  ],
  providers: [
    AuthGuard,
    AuthService,
    SharedService,
    ProgressBarService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
