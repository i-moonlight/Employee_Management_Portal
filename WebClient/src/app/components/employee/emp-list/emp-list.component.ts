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
  employeeList: Employee[];
  activateAddEditEmpComp: boolean;
  employeeListWithoutFilter: Employee[];
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
    this.service.getEmployeeListFromDB().subscribe(res => this.employeeList = res);
  }

  showEmployeePhoto(dataItem: Employee): string {
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

  editEmployee(dataItem: Employee): void {
    this.activateAddEditEmpComp = true;
    this.employee = dataItem;
    this.modalTitle = "Edit Employee";
  }

  showConfirmDeleteEmployee(dataItem: Employee): void {
    if (confirm('Are you sure??'))
      return this.deleteEmployee(dataItem);
  }

  deleteEmployee(dataItem: Employee): void {
    this.service.deleteEmployeeFromDB(dataItem.EmployeeId).subscribe((res: string) => {
      alert(res);
      this.updateEmployeeList();
      console.warn(res);
      },
      (error: string) => console.error(error))
  };

  filterEmployeeList(): void {
    let empIdFilter = this.employeeIdFilter;
    let empNameFilter = this.employeeNameFilter;
    let empDepartmentFilter = this.employeeDepartmentFilter;
    let empNameDateOfJoiningFilter = this.employeeDateOfJoiningFilter;

    this.employeeList = this.employeeListWithoutFilter.filter((emp) => {
      return emp.EmployeeId.toString().toLowerCase().includes(empIdFilter.toLowerCase()) &&
        emp.EmployeeName.toString().toLowerCase().includes(empNameFilter.toLowerCase()) &&
        emp.Department.toString().toLowerCase().includes(empDepartmentFilter.toLowerCase()) &&
        emp.DateOfJoining.toString().toLowerCase().includes(empNameDateOfJoiningFilter.toLowerCase())
    });
  }

  sortEmployeeList(prop: string, asc: boolean): void {
    this.employeeList = this.employeeList.sort((a, b) => {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }
}
