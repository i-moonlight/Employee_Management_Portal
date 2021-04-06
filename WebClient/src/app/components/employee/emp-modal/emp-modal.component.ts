import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.component.html',
  styleUrls: ['./emp-modal.component.css']
})
export class EmployeeModalComponent implements OnInit {
  @Input() emp: any;
  employeeId: string;
  employeeName: string;
  department: string;
  dateOfJoining: string;
  departmentsList: any = [];

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.loadDepartmentList()
  }

  private loadDepartmentList(): void {
    this.service.getAllDepartmentNames().subscribe((res: any) => {
      this.departmentsList = res;
      this.employeeId = this.emp.EmployeeId;
      this.employeeName = this.emp.EmployeeName;
      this.department = this.emp.Department;
      this.dateOfJoining = this.emp.DateOfJoining;
    });
  }

  addEmployee(): void {
    let object = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining
    };
    this.service.addEmployee(object).subscribe(res => alert(res.toString()));
  }

  updateEmployee(): void {
    let object = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining
    };
    this.service.updateEmployee(object).subscribe(res => alert(res.toString()));
  }
}
