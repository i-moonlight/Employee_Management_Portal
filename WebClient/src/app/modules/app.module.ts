import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from '../app.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeComponent } from '../components/employee/employee.component';
import { EmployeeListComponent } from '../components/employee/emp-list/emp-list.component';
import { EmployeeModalComponent } from '../components/employee/emp-modal/emp-modal.component';
import { DepartmentComponent } from '../components/department/department.component';
import { DepartmentListComponent } from '../components/department/dep-list/dep-list.component';
import { DepartmentModalComponent} from '../components/department/dep-modal/dep-modal.component';
import { SharedService } from '../services/shared/shared.service';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { environment } from "../../environments/environment";
import { AUTH_API_URL } from "../app-injection-tokens";
import { JwtModule } from "@auth0/angular-jwt";
import { ACCESS_TOKEN_KEY } from "../services/authentication/auth.service";

@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeModalComponent,
    DepartmentComponent,
    DepartmentListComponent,
    DepartmentModalComponent
  ],

  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,

    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem(ACCESS_TOKEN_KEY),
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],

  providers: [{provide: AUTH_API_URL, useValue: environment.authApi}, SharedService],
  bootstrap: [AppComponent]
})
export class AppModule {}
