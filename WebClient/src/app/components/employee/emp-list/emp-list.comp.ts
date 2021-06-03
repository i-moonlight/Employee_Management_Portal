import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { IEmployee } from '../emp.comp';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.comp.html',
  styleUrls: ['./emp-list.comp.css']
})
export class EmployeeListComponent implements OnInit {
  employee: IEmployee;
  employeeList: IEmployee[];
  modalTitle: string;
  activateAddEditEmpComp: boolean;

  constructor(private service: SharedService) {
    this.activateAddEditEmpComp = false;
  }

  ngOnInit(): void {
    this.updateEmployeeList();
  }

  updateEmployeeList(): void {
    this.service.getEmployeeListFromDB().subscribe((response: IEmployee[]) => {
      this.employeeList = response
    });
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

  editEmployee(dataItem: IEmployee): void {
    this.employee = dataItem;
    this.modalTitle = "Edit Employee";
    this.activateAddEditEmpComp = true;
    console.warn(dataItem);
  }

  showConfirmDeleteEmployee(dataItem: IEmployee): void {
    if (confirm('Are you sure??'))
      return this.deleteEmployee(dataItem);
  }

  deleteEmployee(dataItem: IEmployee): void {
    this.service.deleteEmployeeFromDB(dataItem.EmployeeId).subscribe(
      (response: string) => {
        alert(response);
        this.updateEmployeeList();
        console.warn(response);
      },
      (error: string) => {
        console.error(error);
      })
  };
}
