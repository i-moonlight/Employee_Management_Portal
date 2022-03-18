import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '@services/authentication/auth.service';
import { Dto } from '@models/dto.model';
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
    public progressBar: ProgressBarService,
    private toaster: ToastrService) {}

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

  public forgotPassword(emailFormValue): void {
    this.progressBar.startLoading();

    const dto: Dto = {
      Email: emailFormValue.email,
      ResetPasswordUrl: "http://localhost:4200/auth/forgot-password"
    }

    this.authService.sendForgotPasswordEmail(dto).subscribe(
      (response) => {
        this.progressBar.setSuccess();
        this.progressBar.completeLoading();
        console.warn(response);
      },
      (err: HttpErrorResponse) => {
        this.progressBar.setError();
        this.progressBar.completeLoading();
        console.error(err);
      });
  }
}
