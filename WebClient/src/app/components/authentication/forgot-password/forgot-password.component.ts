import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '@services/authentication/auth.service';
import { Dto } from '@models/dto.model';
import { NotificationService } from '@services/notification.service';
import { Pattern } from '@app/app.constants';
import { ProgressBarService } from '@services/progress-bar/progress-bar.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  public emailForm!: FormGroup;

  constructor(
    private authService: AuthService,
    private progressBar: ProgressBarService,
    private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.emailForm = new FormGroup({
      email: new FormControl('', [
        Validators.email,
        Validators.required,
        Validators.pattern(Pattern.EMAIL_PATTERN)
      ]),
    });
  }

  public get email(): AbstractControl {
    return this.emailForm.controls['email'];
  }

  public forgotPassword(forgotPas: Account) {
    const email: { Email: string } = {
      Email: forgotPas.Email
    };
  }

  public forgotPassword(emailFormValue): void {
    this.progressBar.startLoading();

    const dto: Dto = {
      Email: emailFormValue.email,
      ResetPasswordUrl: 'http://localhost:4200/auth/forgot-password'
    };

    this.authService.sendForgotPasswordEmail(dto).subscribe(
      (res) => {
        if (res.IsValid == true) {
          this.progressBar.setSuccess();
          this.progressBar.completeLoading();
          this.notificationService.setSuccessMessage(res.Message);
          console.warn(res);
        } else {
          this.progressBar.setError();
          this.progressBar.completeLoading();
          this.notificationService.setErrorMessage(res.Message);
          console.error(res);
        }
      },
      error: (err: HttpErrorResponse) => {}
    });

      (err: HttpErrorResponse) => {
        this.progressBar.setError();
        this.progressBar.completeLoading();
        this.notificationService.setErrorMessage('Server connection failed');
        console.error(err);
      });
  }
}
