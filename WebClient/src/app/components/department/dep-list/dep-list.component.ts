import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { Department } from '../department.component';

@Component({
  selector: 'app-dep-list',
  templateUrl: './dep-list.component.html',
  styleUrls: ['./dep-list.component.css']
})
export class DepartmentListComponent implements OnInit {
  activateDepModalComp: boolean;
  department: Department;
  departmentIdFilter: string;
  departmentNameFilter: string;
  departmentList: Department[];
  departmentListWithoutFilter: Department[];
  modalTitle: string;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.activateDepModalComp = false;
    this.departmentIdFilter = '';
    this.departmentNameFilter = '';
    this.departmentListWithoutFilter = [];
    this.updateDepartmentList();
  }

  updateDepartmentList(): void {
    this.service.getDepartmentListFromDB().subscribe((res: Department[]) => {
      this.departmentList = res;
      this.departmentListWithoutFilter = res;
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

  editDepartment(dataItem: Department): void {
    this.activateDepModalComp = true;
    this.department = dataItem;
    this.modalTitle = 'Edit Department';
  }


  closeDepartmentModal(): void {
    this.activateDepModalComp = false
    this.updateDepartmentList();
  }

  showDeleteConfirm(dataItem: Department): void {
    if (confirm('Are you sure??'))
      this.deleteDepartment(dataItem);
  }

  deleteDepartment(dataItem: Department): void {
    this.service.deleteDepartmentFromDB(dataItem.DepartmentId).subscribe(
      (res: string) => {
        alert(res);
        this.updateDepartmentList();
        console.warn(res);
      },
      (error: string) => {
        console.error(error);
      }
    );
  }

  toFilterDepartmentList(): void {
    let depIdFilter = this.departmentIdFilter;
    let depNameFilter = this.departmentNameFilter;

    this.departmentList = this.departmentListWithoutFilter.filter((dep: Department) => {
      return dep.DepartmentId.toString()
        .toLowerCase()
        .includes(depIdFilter.toString().trim().toLowerCase())
      &&
      dep.DepartmentName.toString()
        .toLowerCase()
        .includes(depNameFilter.toString().trim().toLowerCase())
    });
  }

  toSortDepartmentList(prop: string, asc: boolean): void {
    this.departmentList = this.departmentListWithoutFilter.sort((a, b) => {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }
}
