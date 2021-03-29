import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-dep-modal',
  templateUrl: './dep-modal.component.html',
  styleUrls: ['./dep-modal.component.css']
})
export class DepartmentModalComponent implements OnInit {
  @Input() dep: any;

  ngOnInit(): void {}
}
