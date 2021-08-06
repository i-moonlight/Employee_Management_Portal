import { Component } from '@angular/core';
import { AuthService } from './services/authentication/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  appTitle = 'Employee Management Portal';
  private as: AuthService;

  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }
}
