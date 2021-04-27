import { Component, OnInit } from '@angular/core';

export interface Department {
  DepartmentId: number;
  DepartmentName: string;
}

@Component({
  selector: 'app-dep',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {}
}
