import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { Department } from '../department.component';

@Component({
  selector: 'app-dep-modal',
  templateUrl: './dep-modal.component.html',
  styleUrls: ['./dep-modal.component.css']
})
export class DepartmentModalComponent implements OnInit {
  @Input() dep: Department;
  departmentId: number;
  departmentName: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.departmentId = this.dep.DepartmentId;
    this.departmentName = this.dep.DepartmentName;
  }

  getDepartment(): Department {
    return this.dep = {
      DepartmentId: this.departmentId,
      DepartmentName: this.departmentName
    };
  }

  addDepartment(): void {
    this.service.addDepartmentToDB(this.getDepartment()).subscribe((res: string) => alert(res));
  }

  updateDepartment(): void {
    this.service.updateDepartmentToDB(this.getDepartment()).subscribe((res: string) => alert(res));
  }
}
