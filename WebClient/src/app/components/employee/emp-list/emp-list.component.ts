import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { Employee } from '../employee.component';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.component.html',
  styleUrls: ['./emp-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employee: Employee;
  employeeList: string[];
  modalTitle: string;
  activateAddEditEmpComp: boolean;

  constructor(private service: SharedService) {
    this.activateAddEditEmpComp = false;
  }

  ngOnInit(): void {
    this.updateEmployeeList();
  }

  updateEmployeeList(): void {
    this.service.getEmployeeListFromDB().subscribe(res => this.employeeList = res);
  }

  addEmployee(): void {
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

  closeEmployeeModal(): void {
    this.updateEmployeeList();
    this.activateAddEditEmpComp = false;
  }

  editEmployee(dataItem: Employee): void {
    this.employee = dataItem;
    this.modalTitle = "Edit Employee";
    this.activateAddEditEmpComp = true;
    console.warn(dataItem);
  }

  deleteEmployee(dataItem: Employee): void {
    this.service.deleteEmployeeFromDB(dataItem.EmployeeId).subscribe(
      (res: string) => {
        alert(res);
        this.updateEmployeeList();
        console.warn(res);
      },
      (error: string) => console.error(error))
  }
}
