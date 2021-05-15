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
    this.service.getEmployeeListFromDB().subscribe(response => this.employeeList = response);
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

  deleteEmployee(dataItem: IEmployee): void {
    if (confirm('Are you sure??')) {
      this.service.deleteEmployeeFromDB(dataItem.EmployeeId).subscribe((dataItem: string) => {
        try {
          alert(dataItem);
          this.updateEmployeeList();
          console.warn('Employee deleted!')
        } catch (e) {
          e.console.error('Employee not deleted!')
        }
      });
    }
  }
}
