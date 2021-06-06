import { Component, OnInit } from '@angular/core';

export interface IDepartment {
  DepartmentId: number;
  DepartmentName: string;
}

@Component({
  selector: 'app-dep',
  templateUrl: './dep.comp.html',
  styleUrls: ['./dep.comp.css']
})
export class DepartmentComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {}
}
