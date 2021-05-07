import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-dep-list',
  templateUrl: './dep-list.comp.html',
  styleUrls: ['./dep-list.comp.css']
})
export class DepartmentListComponent implements OnInit {
  departmentList: any = [];
  dep: any;
  activateAddEditDepComp: boolean = false;
  modalTitle: string;

  departmentIdFilter: string = '';
  departmentNameFilter: string = '';
  departmentListWithoutFilter: any = [];

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.updateDepartmentList();
  }

  updateDepartmentList(): void {
    this.service.getDepartmentList().subscribe(data => {
      this.departmentList = data;
      this.departmentListWithoutFilter = data;
    });
  }

  addClick(): void {
    this.dep = {
      DepartmentId: 0,
      DepartmentName: ''
    };
    this.modalTitle = 'Add Department';
    this.activateAddEditDepComp = true;
  }

  closeClick(): void {
    this.updateDepartmentList();
    this.activateAddEditDepComp = false;
  }

  editClick(item: any): void {
    this.dep = item;
    this.modalTitle = 'Edit Department';
    this.activateAddEditDepComp = true;
  }

  deleteClick(item): void {
    if (confirm('Are you sure?')) {
      this.service.deleteDepartment(item.DepartmentId).subscribe(data => {
        alert(data.toString());
        this.updateDepartmentList();
      });
    }
  }

  filterData() {
    let depIdFilter = this.departmentIdFilter;
    let depNameFilter = this.departmentNameFilter;

    this.departmentList = this.departmentListWithoutFilter.filter(function (el) {
      return el.DepartmentId.toString().toLowerCase()
          .includes(depIdFilter.toString().trim().toLowerCase())
        &&
        el.DepartmentName.toString().toLowerCase()
          .includes(depNameFilter.toString().trim().toLowerCase())
    });
  }

  sortResult(prop: string, asc: boolean) {
    this.departmentList = this.departmentListWithoutFilter.sort(function (a, b) {
      if (asc) {
        return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0);
      } else {
        return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0);
      }
    });
  }
}
