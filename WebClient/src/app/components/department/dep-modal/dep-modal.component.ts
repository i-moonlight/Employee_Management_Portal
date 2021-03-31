import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-dep-modal',
  templateUrl: './dep-modal.component.html',
  styleUrls: ['./dep-modal.component.css']
})
export class DepartmentModalComponent implements OnInit {
  @Input() dep: any;
  departmentId: string;
  departmentName: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.departmentId = this.dep.DepartmentId;
    this.departmentName = this.dep.DepartmentName;
  }

  addDepartment(): void {
    let object = {
      DepartmentId: this.departmentId,
      DepartmentName: this.departmentName
    };
    this.service.addDepartment(object).subscribe(res => alert(res.toString()));
  }

  updateDepartment(): void {
    let object = {
      DepartmentId: this.departmentId,
      DepartmentName: this.departmentName
    };
    this.service.updateDepartment(object).subscribe(res => alert(res.toString()));
  }
}
