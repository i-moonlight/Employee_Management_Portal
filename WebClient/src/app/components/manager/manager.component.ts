import { Component, OnInit } from '@angular/core';
import { Account } from '../../models/account.model';
import { AuthService } from '../../services/authentication/auth.service';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrls: ['./manager.component.css']
})
export class ManagerComponent implements OnInit {
  public userList: Account[] = [];

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.getAllUser();
  }

  private getAllUser() {
    // this.authService.getUserList().subscribe((response: User[]) => {
    //   this.userList = response;
    // })
  }
}