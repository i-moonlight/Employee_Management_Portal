import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.comp.html',
  styleUrls: ['./emp-modal.comp.css']
})
export class EmployeeModalComponent implements OnInit {
  @Input() emp: any;

  constructor() {}

  ngOnInit(): void {}
}
