import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeListComponent } from './emp-list.comp';
import { SharedService } from '../../../services/shared/shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';
import { IEmployee } from '../emp.comp';

describe('EmployeeListComponent', () => {
  let component: EmployeeListComponent;
  let fixture: ComponentFixture<EmployeeListComponent>;
  let service: SharedService;
  let mockList: IEmployee[];
  let mock: IEmployee;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeListComponent],
      imports: [HttpClientModule, FormsModule],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    mock = <IEmployee>{
      EmployeeId: 0, EmployeeName: '', Department: '', DateOfJoining: '', PhotoFileName: 'anonymous.png'
    };
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

  it('should set employee value when add employee', () => {
    component.addEmployee();
    expect(component.employee).toEqual(mock);
  });

  it('should set activate component value when add employee', () => {
    component.addEmployee();
    expect(component.activateAddEditEmpComp).toBeTrue();
  });

  it('should set modal title value as `Add Employee` when add employee', () => {
    component.addEmployee();
    expect(component.modalTitle).toEqual('Add Employee');
  });

  it('should call confirm window when show confirm', () => {
    const spy = spyOn(window, 'confirm');
    component.showConfirmDeleteEmployee(mock);
    expect(spy).toHaveBeenCalled();
  });

  it('should call shared service when delete employee', () => {
    const spy = spyOn(service, 'deleteEmployeeFromDB').and.returnValue(of('Delete successful'));
    component.deleteEmployee(mock);
    expect(spy.calls.any()).toBeTruthy();
  });
});
