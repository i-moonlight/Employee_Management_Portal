import { Component, OnInit } from '@angular/core';

export interface IEmployee {
  EmployeeId: number;
  EmployeeName: string;
  Department: string;
  DateOfJoining: string;
  PhotoFileName: string;
}

@Component({
  selector: 'app-emp',
  templateUrl: './emp.comp.html',
  styleUrls: ['./emp.comp.css']
})
export class EmployeeComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {}
}
