import { ToastrService } from 'ngx-toastr';

import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { Account } from '@models/account.model';
import { AuthService } from '@services/authentication/auth.service';
import { Pattern } from '@constants/app.constants';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  public forgotPasswordForm!: FormGroup;

  constructor(private authService: AuthService, private toasterService: ToastrService) {}

  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl('', [
        Validators.email,
        Validators.required,
        Validators.pattern(Pattern.EMAIL_PATTERN)
      ]),
    });
  }

  public get email(): AbstractControl {
    return this.forgotPasswordForm.controls['email'];
  }

  public forgotPassword(forgotPas: Account) {
    const email: { Email: string } = {
      Email: forgotPas.Email
    };

    this.authService.forgotPassword(email).subscribe({
      next: (_) => {
        // this.showSuccess = true;
        // this.successMessage = 'The link has been sent, please check your email to reset your password.'
      },
      error: (err: HttpErrorResponse) => {
        // this.showError = true;
        // this.errorMessage = err.message;
      }
    });
  }
}
