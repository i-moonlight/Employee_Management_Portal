import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.comp.html',
  styleUrls: ['./emp-list.comp.css']
})
export class EmployeeListComponent implements OnInit {
  employeeList: any = [];

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.updateEmployeeList();
  }

  updateEmployeeList(): void {
    this.service.getEmployeeList().subscribe(data => this.employeeList = data);
  }
}
