import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { of, throwError } from 'rxjs';
import { Employee } from '../employee.component';
import { EmployeeListComponent } from './emp-list.component';
import { SharedService } from '../../../services/shared/shared.service';

describe('EmployeeListComponent', () => {
  let component: EmployeeListComponent;
  let fixture: ComponentFixture<EmployeeListComponent>;
  let service: SharedService;
  let mockList: Employee[];
  let mock: Employee;

  beforeEach(async() => {
    await TestBed
      .configureTestingModule({
        declarations: [EmployeeListComponent],
        imports: [HttpClientModule, FormsModule],
        schemas: [CUSTOM_ELEMENTS_SCHEMA],
      })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    mock = <Employee>{};
    fixture.detectChanges();
  });

  it('should create employee list component', () => {
    expect(component).toBeTruthy();
  });

  it('should call shared service when update employee list', () => {
    const spy = spyOn(service, 'getEmployeeListFromDB').and.returnValue(of(mockList));
    component.updateEmployeeList();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should set employee list value when update employee list', () => {
    spyOn(service, 'getEmployeeListFromDB').and.returnValue(of(mockList));
    component.updateEmployeeList();
    expect(component.employeeList).toEqual(mockList);
  });

  it('should call show employee photo method with param', () => {
    const spy = spyOn(component, 'showEmployeePhoto').and.returnValue('path');
    component.showEmployeePhoto(mock);
    expect(spy).toHaveBeenCalledWith(mock);
  });

  it('should set employee value when show employee photo', () => {
    component.showEmployeePhoto(mock);
    expect(component.employee).toEqual(mock);
  });

  it('should call close employee modal method when click on button', () => {
    const spy = spyOn(component, 'closeEmployeeModal');
    const btn = fixture.debugElement.query(By.css('.btn-close'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should set component activate value when close employee modal', () => {
    component.closeEmployeeModal();
    expect(component.activateAddEditEmpComp).toBeFalse();
  });

  it('should call update employee list when close employee modal', () => {
    const spy = spyOn(component, 'updateEmployeeList');
    component.closeEmployeeModal();
    expect(spy).toHaveBeenCalled();
  });

  it('should set component activate value when edit employee', () => {
    component.editEmployee(mock);
    expect(component.activateAddEditEmpComp).toBeTrue();
  });

  it('should set employee value when edit employee', () => {
    component.editEmployee(mock);
    expect(component.employee).toEqual(mock);
  });

  it('should set modal title value as `Edit Employee` when edit employee', () => {
    component.editEmployee(mock);
    expect(component.modalTitle).toEqual('Edit Employee');
  });

  it('should call confirm window when show confirm', () => {
    const spy = spyOn(window, 'confirm');
    component.showConfirmDeleteEmployee(mock);
    expect(spy).toHaveBeenCalled();
  });

  it('should call delete employee method when positive result confirm', () => {
    const spy = spyOn(component, 'deleteEmployee');
    component.showConfirmDeleteEmployee(mock);
    expect(spy).toHaveBeenCalledWith(mock);
  });

  it('should call shared service when delete employee', () => {
    const spy = spyOn(service, 'deleteEmployeeFromDB').and.returnValue(of('Delete successful'));
    component.deleteEmployee(mock);
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should call error console when delete employee', () => {
    const spy = spyOn(console, 'error');
    spyOn(service, 'deleteEmployeeFromDB').and.returnValue(throwError('Delete was not successful'));
    component.deleteEmployee(mock);
    expect(spy).toHaveBeenCalledWith('Delete was not successful');
  });

  it('should call employee list filter method when input data', () => {
    const spy = spyOn(component, 'filterEmployeeList');
    const btn = fixture.debugElement.query(By.css('.form-control'));
    btn.triggerEventHandler('keyup', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should set employee list value when filter employee list', () => {
    component.filterEmployeeList();
    expect(component.employeeList).toEqual(mockList);
  });

  it('should call filter method when filter employee list', () => {
    const spy = spyOn(component.employeeListWithoutFilter, 'filter');
    component.filterEmployeeList();
    expect(spy).toHaveBeenCalled();
  });

  it('should call employee list sort method when click on button', () => {
    const spy = spyOn(component, 'sortEmployeeList');
    const btn = fixture.debugElement.query(By.css('.sort'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should set employee list value when sort employee list', () => {
    component.sortEmployeeList('test', true);
    expect(component.employeeList).toEqual(mockList);
  });

  it('should call sort method when sort employee list', () => {
    const spy = spyOn(component.employeeList, 'sort');
    component.sortEmployeeList(' ', true);
    expect(spy).toHaveBeenCalled();
  });
});
