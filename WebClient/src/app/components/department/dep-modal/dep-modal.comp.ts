import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { IDepartment } from '../dep.comp';

@Component({
  selector: 'app-dep-modal',
  templateUrl: './dep-modal.comp.html',
  styleUrls: ['./dep-modal.comp.css']
})
export class DepartmentModalComponent implements OnInit {
  @Input() dep: IDepartment;
  departmentId: number;
  departmentName: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.departmentId = this.dep.DepartmentId;
    this.departmentName = this.dep.DepartmentName;
  }

  getDepartment(): IDepartment {
    return this.dep = {
      DepartmentId: this.departmentId,
      DepartmentName: this.departmentName
    };
  }

  addDepartment(): void {
    this.service.addDepartmentToDB(this.getDepartment())
      .subscribe((response: string) => alert(response));
  }

  updateDepartment(): void {
    this.service.updateDepartmentToDB(this.getDepartment())
      .subscribe((response: string) => alert(response));
  }
}
