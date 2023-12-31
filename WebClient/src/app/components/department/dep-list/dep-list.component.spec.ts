import { By } from '@angular/platform-browser';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { DepartmentListComponent } from './dep-list.component';
import { SharedService } from '../../../services/shared/shared.service';
import { Department } from '../department.component';

describe('DepartmentListComponent', () => {
  let component: DepartmentListComponent;
  let fixture: ComponentFixture<DepartmentListComponent>;
  let service: SharedService;
  let mockList: Department[];
  let mock: Department;

  beforeEach(async() => {
    await TestBed
      .configureTestingModule({
        declarations: [DepartmentListComponent],
        imports: [HttpClientModule, FormsModule],
        schemas: [CUSTOM_ELEMENTS_SCHEMA],
      })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    mock = <Department>{ DepartmentId: 0, DepartmentName: '' };
    fixture.detectChanges();
  });

  it('should create department list component', () => {
    expect(component).toBeTruthy();
  });

  it('should deactivate department modal component when initialize department list component', () => {
    expect(component.activateDepModalComp).toBeFalse();
  });

  it('should set department filter id value when initialize department list component', () => {
    expect(component.departmentIdFilter).toEqual('');
  });

  it('should set department filter name value when initialize department list component', () => {
    expect(component.departmentNameFilter).toEqual('');
  });

  it('should set call update department list when initialize department list component', () => {
    const spy = spyOn(component, 'updateDepartmentList');
    component.ngOnInit();
    expect(spy).toHaveBeenCalled();
  });

  it('should call shared service when update department list', () => {
    const spy = spyOn(service, 'getDepartmentListFromDB').and.returnValue(of(mockList));
    component.updateDepartmentList();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should set department list value when update department list', () => {
    spyOn(service, 'getDepartmentListFromDB').and.returnValue(of(mockList));
    component.updateDepartmentList();
    expect(component.departmentList).toEqual(mockList);
  });

  it('should call add department method when click on button', () => {
    const spy = spyOn(component, 'addDepartment');
    const btn = fixture.debugElement.query(By.css('.btn-float'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should activate modal department component when add department', () => {
    component.addDepartment();
    expect(component.activateDepModalComp).toBeTrue();
  });

  it('should set department object values when add department', () => {
    component.addDepartment();
    expect(component.department).toEqual(mock);
  });

  it('should have modal title value as `Add Department` when add department', () => {
    component.addDepartment();
    expect(component.modalTitle).toEqual('Add Department');
  });

  it('should activate department modal component when edit department', () => {
    component.editDepartment(mock);
    expect(component.activateDepModalComp).toBeTrue();
  });

  it('should set department object value when edit department', () => {
    component.editDepartment(mock);
    expect(component.department).toEqual(mock);
  });

  it('should have modal title value as `Edit Department` when edit department', () => {
    component.editDepartment(mock);
    expect(component.modalTitle).toEqual('Edit Department');
  });

  it('should call close department modal method when click on close button', () => {
    const spy = spyOn(component, 'closeDepartmentModal');
    const btn = fixture.debugElement.query(By.css('.btn-close'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should deactivate department modal component when close department modal', () => {
    component.closeDepartmentModal();
    expect(component.activateDepModalComp).toBeFalse();
  });

  it('should call update department list method when close department modal', () => {
    const spy = spyOn(component, 'updateDepartmentList');
    component.closeDepartmentModal();
    expect(spy).toHaveBeenCalled();
  });

  it('should call confirm window when show delete confirm', () => {
    const spy = spyOn(window, 'confirm');
    component.showDeleteConfirm(mock);
    expect(spy).toHaveBeenCalledWith('Are you sure??');
  });

  it('should call delete department method when click on confirm button', () => {
    const spy = spyOn(component, 'deleteDepartment');
    component.showDeleteConfirm(mock);
    expect(spy).toHaveBeenCalledWith(mock);
  });

  it('should call shared service when delete department', () => {
    const spy = spyOn(service, 'deleteDepartmentFromDB').and.returnValue(of('Delete successful'));
    component.deleteDepartment(mock);
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should call alert window when delete department', () => {
    const spy = spyOn(window, 'alert');
    spyOn(service, 'deleteDepartmentFromDB').and.returnValue(of('Delete successful'));
    component.deleteDepartment(mock);
    expect(spy).toHaveBeenCalledWith('Delete successful');
  });

  it('should call update department list method when delete department', () => {
    const spy = spyOn(component, 'updateDepartmentList');
    spyOn(service, 'deleteDepartmentFromDB').and.returnValue(of('Delete successful'));
    component.deleteDepartment(mock);
    expect(spy).toHaveBeenCalled();
  });

  it('should call warn console when delete department', () => {
    const spy = spyOn(console, 'warn');
    spyOn(service, 'deleteDepartmentFromDB').and.returnValue(of('Delete successful'));
    component.deleteDepartment(mock);
    expect(spy).toHaveBeenCalledWith('Delete successful');
  });

  it('should call error console when delete department exception', () => {
    const spy = spyOn(console, 'error');
    spyOn(service, 'deleteDepartmentFromDB').and.returnValue(throwError('Delete was not successful'));
    component.deleteDepartment(mock);
    expect(spy).toHaveBeenCalledWith('Delete was not successful');
  });

  it('should call department list filter method when input data', () => {
    const spy = spyOn(component, 'filterDepartmentList');
    const btn = fixture.debugElement.query(By.css('.form-control'));
    btn.triggerEventHandler('keyup', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should set department list value when filter department list', () => {
    component.filterDepartmentList();
    expect(component.departmentList).toEqual(mockList);
  });

  it('should call filter method when filter department list', () => {
    const spy = spyOn(component.departmentListWithoutFilter, 'filter');
    component.filterDepartmentList();
    expect(spy).toHaveBeenCalled();
  });

  it('should call department list sort method when click on sort button', () => {
    const spy = spyOn(component, 'sortDepartmentList');
    const btn = fixture.debugElement.query(By.css('.sort'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should set department list value when sort department list', () => {
    component.sortDepartmentList('test', true);
    expect(component.departmentList).toEqual(mockList);
  });
});
