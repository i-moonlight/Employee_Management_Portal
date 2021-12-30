import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { IDepartment } from '../dep.comp';

@Component({
  selector: 'app-dep-list',
  templateUrl: './dep-list.comp.html',
  styleUrls: ['./dep-list.comp.css']
})
export class DepartmentListComponent implements OnInit {
  activateDepModalComp: boolean;
  department: IDepartment;
  departmentIdFilter: string;
  departmentNameFilter: string;
  departmentList: IDepartment[];
  departmentListWithoutFilter: IDepartment[];
  modalTitle: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.activateDepModalComp = false;
    this.departmentIdFilter = '';
    this.departmentNameFilter = '';
    this.departmentList = [];
    this.departmentListWithoutFilter = [];
    this.updateDepartmentList();
  }

  updateDepartmentList(): void {
    this.service.getDepartmentListFromDB().subscribe((response: IDepartment[]) => {
      this.departmentList = response;
      this.departmentListWithoutFilter = response;
    });
  }

  addDepartment(): void {
    this.activateDepModalComp = true;
    this.department = {
      DepartmentId: 0,
      DepartmentName: ''
    };
    this.modalTitle = 'Add Department';
  }

  editDepartment(dataItem: IDepartment): void {
    this.activateDepModalComp = true;
    this.department = dataItem;
    this.modalTitle = 'Edit Department';
  }

  closeDepartmentModal(): void {
    this.activateDepModalComp = false
    this.updateDepartmentList();
  }

  showDeleteConfirm(dataItem: IDepartment): void {
    if (confirm('Are you sure??'))
      this.deleteDepartment(dataItem);
  }

  deleteDepartment(dataItem: IDepartment): void {
    this.service.deleteDepartmentFromDB(dataItem.DepartmentId).subscribe(
      (response: string) => {
        alert(response);
        this.updateDepartmentList();
        console.warn(response);
      },
      (error: string) =>
        console.error(error));
  }

  toFilterDepartmentList(): void {
    let depIdFilter = this.departmentIdFilter;
    let depNameFilter = this.departmentNameFilter;

    this.departmentList = this.departmentListWithoutFilter.filter((department: IDepartment) => {
      return department.DepartmentId
          .toString()
          .toLowerCase()
          .includes(depIdFilter.toString().trim().toLowerCase())
        &&
        department.DepartmentName
          .toString()
          .toLowerCase()
          .includes(depNameFilter.toString().trim().toLowerCase())
    });
  }

  toSortDepartmentList(prop: string, asc: boolean): void {
    this.departmentList = this.departmentList.sort((a, b) => {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }
}
