import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
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
    RegisterComponent,
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

    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem(ACCESS_TOKEN_KEY),
        allowedDomains:   ['localhost:9090'],
        disallowedRoutes: ['localhost:9090/api/auth']
      }
    })
  ],

  providers: [
    SharedService,
    AuthService,
    AuthGuard,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}],
    bootstrap: [AppComponent]
})

export class AppModule {}
