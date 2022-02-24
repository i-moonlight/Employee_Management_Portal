import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from '../app.comp';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { EmployeeComponent } from '../components/employee/emp.comp';
import { EmployeeListComponent } from '../components/employee/emp-list/emp-list.comp';
import { EmployeeModalComponent } from '../components/employee/emp-modal/emp-modal.comp';
import { DepartmentComponent } from '../components/department/dep.comp';
import { DepartmentListComponent } from '../components/department/dep-list/dep-list.comp';
import { DepartmentModalComponent} from '../components/department/dep-modal/dep-modal.comp';
import { SharedService } from '../services/shared/shared.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from '../components/authentication/registration/register.component';
import { ManagerComponent } from '../components/manager/manager.component';
import { LoginComponent } from '../components/authentication/login/login.component';
import { AuthComponent } from '../components/authentication/auth.component';
import { AuthService } from '../services/authentication/auth.service';
import { AuthGuard } from '../guards/auth.guard';
import { AuthInterceptor } from '../guards/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent,
    RegisterComponent,
    ManagerComponent,
    LoginComponent
  ],

  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],

  providers: [
    SharedService,
    AuthService,
    AuthGuard,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}],

  bootstrap: [AppComponent]
})
export class AppModule {}
