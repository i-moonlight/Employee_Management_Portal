import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-dep-modal',
  templateUrl: './dep-modal.comp.html',
  styleUrls: ['./dep-modal.comp.css']
})
export class DepartmentModalComponent implements OnInit {
  @Input() dep: any;

  constructor() {}

  ngOnInit(): void {}
}
