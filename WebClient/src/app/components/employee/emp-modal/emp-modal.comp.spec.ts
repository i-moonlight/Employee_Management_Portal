import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeModalComponent } from './emp-modal.comp';
import { SharedService } from '../../../services/shared/shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';
import { IEmployee } from '../emp.comp';
import { By } from '@angular/platform-browser';

describe('EmployeeModalComponent', () => {
  let component: EmployeeModalComponent;
  let fixture: ComponentFixture<EmployeeModalComponent>;
  let service: SharedService;
  let mockList: string[];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeModalComponent],
      imports: [HttpClientModule, FormsModule],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeModalComponent);
    component = fixture.componentInstance;
    component.emp = <IEmployee>{};
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    fixture.detectChanges();
  });

  it('should create employee modal component', () => {
    expect(component).toBeTruthy();
  });

  it('should call shared service when load department list', () => {
    const spy = spyOn(service, 'getAllDepartmentNamesFromDB').and.returnValue(of(mockList));
    component.loadDepartmentList();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should set department list value when load department list', () => {
    spyOn(service, 'getAllDepartmentNamesFromDB').and.returnValue(of(mockList));
    component.loadDepartmentList();
    expect(component.departmentList).toEqual(mockList);
  });

  it('should call shared service when add employee', () => {
    const spy = spyOn(service, 'addEmployeeToDB').and.returnValue(of('Created Successfully'));
    component.addEmployee();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should call update employee method when click on update button', () => {
    const spy = spyOn(component, 'updatePhoto');
    const btn = fixture.debugElement.query(By.css('.update'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should call shared service when update employee', () => {
    const spy = spyOn(service, 'updateEmployeeToDB').and.returnValue(of('Update successful'));
    component.updateEmployee();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should call event when select photo file', () => {
    const fakeChangeEvent = new Event('change');
    const spy = spyOn(component, 'onFileSelected');
    const element = document.getElementById('file');
    element.dispatchEvent(fakeChangeEvent);
    expect(spy).toHaveBeenCalledWith(fakeChangeEvent);
  });
});
