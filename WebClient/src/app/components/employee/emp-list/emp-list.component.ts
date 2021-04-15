import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.component.html',
  styleUrls: ['./emp-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employeeList: any = [];
  employee: any;
  modalTitle: string;
  activateAddEditEmpComp: boolean = false;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.updateEmployeeList();
  }

  updateEmployeeList(): void {
    this.service.getEmployeeList().subscribe(res => this.employeeList = res);
  }

  addClick(): void {
    this.employee = {
      EmployeeId: 0,
      EmployeeName: '',
      Department: '',
      DateOfJoining: '',
      PhotoFileName: 'anonymous.png'
    }
    this.modalTitle = 'Add Employee';
    this.activateAddEditEmpComp = true;
  }

  closeClick(): void {
    this.activateAddEditEmpComp = false;
    this.updateEmployeeList();
  }

  editClick(item): void {
    console.log(item);
    this.employee = item;
    this.modalTitle = "Edit Employee";
    this.activateAddEditEmpComp = true;
  }

  deleteClick(item): void {
    if (confirm('Are you sure??')) {
      this.service.deleteEmployee(item.EmployeeId).subscribe(data => {
        alert(data.toString());
        this.updateEmployeeList();
      })
    }
  }
}
