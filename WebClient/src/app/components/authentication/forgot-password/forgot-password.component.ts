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
    })
  }

  public get email(): AbstractControl {
    return this.emailForm.controls['email'];
  }

  public forgotPassword(form: FormGroup): void {
    this.progressBar.startLoading();

    const dto: Dto = {
      Email: form.value.email,
      ResetPasswordUrl: "http://localhost:4200/auth/change-password"
    }

    this.authService.sendForgotPasswordEmail(dto).subscribe((res) => {
      if (res.IsValid) {
        this.progressBar.setSuccess();
        this.notificationService.setSuccessMessage(res.Message);
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
