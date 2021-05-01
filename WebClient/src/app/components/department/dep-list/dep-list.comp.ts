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

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.updateDepartmentList();
  }

  updateDepartmentList(): void {
    this.service.getDepartmentList().subscribe(data => this.departmentList = data);
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
}
