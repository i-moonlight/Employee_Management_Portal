import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { of } from 'rxjs';
import { Department } from '../department.component';
import { SharedService } from '../../../services/shared/shared.service';
import { DepartmentModalComponent } from './dep-modal.component';

describe('DepartmentModalComponent', () => {
  let component: DepartmentModalComponent;
  let fixture: ComponentFixture<DepartmentModalComponent>;
  let mock: Department;
  let service: SharedService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentModalComponent],
      imports: [HttpClientModule, FormsModule],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  })

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentModalComponent);
    component = fixture.componentInstance;
    component.dep = <Department>{};
    mock = <Department>{};
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    fixture.detectChanges();
  })

  it('should create department modal component', () => {
    expect(component).toBeTruthy();
  })

  it('should call shared service when add department', () => {
    const spy = spyOn(service, 'addDepartmentToDB').and.returnValue(of('Create successful'));
    component.addDepartment();
    expect(spy.calls.any()).toBeTruthy();
  })

  it('should call alert window when add department', () => {
    const spy = spyOn(window, 'alert');
    spyOn(service, 'addDepartmentToDB').and.returnValue(of('Create successful'));
    component.addDepartment();
    expect(spy).toHaveBeenCalledWith('Create successful');
  })

  it('should call update department method when click on button', () => {
    const spy = spyOn(component, 'updateDepartment');
    const btn = fixture.debugElement.query(By.css('.btn'));
    btn.triggerEventHandler('click', null);
    expect(spy).toHaveBeenCalled();
  })

  it('should call shared service when update department', () => {
    const spy = spyOn(service, 'updateDepartmentToDB').and.returnValue(of('Update successful'));
    component.updateDepartment();
    expect(spy.calls.any()).toBeTruthy();
  })

  it('should call alert window when update department', () => {
    const spy = spyOn(window, 'alert');
    spyOn(service, 'updateDepartmentToDB').and.returnValue(of('Update successful'));
    component.updateDepartment();
    expect(spy).toHaveBeenCalledWith('Update successful');
  })
})
