import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { IEmployee } from '../emp.comp';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.comp.html',
  styleUrls: ['./emp-list.comp.css']
})
export class EmployeeListComponent implements OnInit {
  activateAddEditEmpComp: boolean;
  employee: IEmployee;
  employeeList: IEmployee[];
  employeeListWithoutFilter: IEmployee[];
  employeeIdFilter: string;
  employeeNameFilter: string;
  employeeDepartmentFilter: string;
  employeeDateOfJoiningFilter: string;
  modalTitle: string;
  photoFilePath: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.activateAddEditEmpComp = false;
    this.employeeList = [];
    this.employeeListWithoutFilter = [];
    this.updateEmployeeList();
  }

  updateEmployeeList(): void {
    this.service.getEmployeeListFromDB().subscribe((response: IEmployee[]) => {
      this.employeeList = response
    });
  }

  showEmployeePhoto(dataItem: IEmployee): string {
    this.employee = dataItem;
    this.photoFilePath = this.service.PhotoUrl + this.employee.PhotoFileName;
    return this.photoFilePath;
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
    this.activateAddEditEmpComp = true;
    this.employee = dataItem;
    this.modalTitle = "Edit Employee";
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

  toFilterEmployeeList(): void {
    let empIdFilter = this.employeeIdFilter;
    let empNameFilter = this.employeeNameFilter;
    let empDepartmentFilter = this.employeeDepartmentFilter;
    let empNameDateOfJoiningFilter = this.employeeDateOfJoiningFilter;

    this.employeeList = this.employeeListWithoutFilter.filter((employee) => {
      return employee.EmployeeId.toString().toLowerCase()
          .includes(empIdFilter.toString().trim().toLowerCase())
        && employee.EmployeeName.toString().toLowerCase()
          .includes(empNameFilter.toString().trim().toLowerCase())
        && employee.Department.toString().toLowerCase()
          .includes(empDepartmentFilter.toString().trim().toLowerCase())
        && employee.DateOfJoining.toString().toLowerCase()
          .includes(empNameDateOfJoiningFilter.toString().trim().toLowerCase())
    });
  }

  toSortEmployeeList(prop: string, asc: boolean): void {
    this.employeeList = this.employeeList.sort((a, b) => {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }
}
