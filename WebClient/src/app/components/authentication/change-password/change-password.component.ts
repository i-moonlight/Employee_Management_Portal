import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

import { Account } from '@models/account.model';
import { AuthService } from '@services/authentication/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from '@services/notification.service';
import { Pattern } from '@app/app.constants';
import { ProgressBarService } from '@services/progress-bar/progress-bar.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
  providers: [CookieService]
})
export class ChangePasswordComponent implements OnInit {
  public passwordForm!: FormGroup;

  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private progressBar: ProgressBarService,
    private router: Router,
    private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.passwordForm = new FormGroup({
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(Pattern.PASSWORD_PATTERN)
      ]),
      confirmPassword: new FormControl('', [
        Validators.required
      ])
    });
  }

  public get password(): AbstractControl {
    return this.passwordForm.controls.password;
  }

  public get confirmPassword(): AbstractControl {
    return this.passwordForm.controls.confirmPassword;
  }

  onCancel(): void {
    if (this.passwordForm != null) this.passwordForm.reset();
  }

  changePassword(form: FormGroup): void {
    this.progressBar.startLoading();

    const dto: Account = {
      Email: this.cookieService.get('email'),
      Password: form.value.password,
    };

    // Passing data to a service
    this.authService.changePassword(dto).subscribe((res) => {
      if (res.IsValid) {
        this.progressBar.setSuccess();
        this.notificationService.setSuccessMessage(res.Message);

        // Redirect to employee URL
        setTimeout(() => this.router.navigate(['/employee']).then(() => window.location.reload()), 2000)

        this.progressBar.completeLoading();
        console.warn(res);
      } else {
        this.progressBar.setError();
        this.notificationService.setErrorMessage(res.Message);
        this.progressBar.completeLoading();
        console.error(res);
      }
    }, (err: HttpErrorResponse) => {
      this.progressBar.setError();
      this.notificationService.setErrorMessage('Server connection failed');
      this.progressBar.completeLoading();
      console.error(err);
    });
  }
}
