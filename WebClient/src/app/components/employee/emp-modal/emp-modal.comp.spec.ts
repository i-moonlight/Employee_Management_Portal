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
  let mock: IEmployee;

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
    mock = <IEmployee>{}
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

  it('should call file reader when select photo file', () => {
    const mockReader: FileReader = jasmine.createSpyObj('FileReader', ['readAsDataURL', 'onload']);
    const mockFile: File = new File([''], 'filename', {type: 'text/html'});
    const mockEvent = {target: {files: [mockFile]}};
    const spy = spyOn(window as any, 'FileReader').and.returnValue(mockReader);
    component.onFileSelected(mockEvent as any);
    expect(spy).toHaveBeenCalled();
    expect(mockReader.readAsDataURL).toHaveBeenCalledWith(mockFile);
  });

  it('should call shared service when upload photo file', () => {
    const spy = spyOn(service, 'uploadPhotoToStorage').and.returnValue(of('photo file name'));
    component.uploadPhoto();
    expect(spy.calls.any()).toBeTruthy();
  });

  it('should set photo name value when upload photo file', () => {
    spyOn(service, 'uploadPhotoToStorage').and.returnValue(of('photo file name'));
    component.uploadPhoto();
    expect(component.photoFileName).toEqual('photo file name');
  });

  it('should call update photo method when click on button', () => {
    const spy = spyOn(component, 'updatePhoto');
    const btn = fixture.debugElement.query(By.css('.but'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  });

  it('should call shared service when update photo file', () => {
    const spy = spyOn(service, 'updatePhotoToStorage').and.returnValue(of(''));
    component.updatePhoto(mock.EmployeeId);
    expect(spy.calls.any()).toBeTruthy();
  });
});
