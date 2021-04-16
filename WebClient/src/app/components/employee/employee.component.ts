import { Component, OnInit } from '@angular/core';

export interface Employee {
  EmployeeId: number;
  EmployeeName: string;
  Department: string;
  DateOfJoining: string;
  PhotoFileName: string;
}

@Component({
  selector: 'app-emp',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  ngOnInit(): void {}
}
