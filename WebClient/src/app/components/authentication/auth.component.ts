import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SharedService } from '../../services/shared/shared.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  private authForm;

  constructor(private formBuilder: FormBuilder, private sharedService: SharedService) {}

  ngOnInit(): void {
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  onSubmit() {
    console.log('on submit')
    let email = this.authForm.controls['email'].value;
    let password = this.authForm.controls['password'].value;

    this.sharedService.toAuthentication(email, password).subscribe((data) => {
        console.log("response", data);
      },
      error => {
        console.log("error", error)
      })
  }
}
