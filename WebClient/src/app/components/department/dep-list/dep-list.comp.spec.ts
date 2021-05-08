import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DepartmentListComponent } from './dep-list.comp';
import { SharedService } from '../../../services/shared/shared.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { of } from 'rxjs';

describe('DepartmentListComponent', () => {
  let component: DepartmentListComponent;
  let fixture: ComponentFixture<DepartmentListComponent>;
  let service: SharedService;
  let mockList: any[];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentListComponent],
      imports: [HttpClientModule, FormsModule],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentListComponent);
    component = fixture.componentInstance;
    service = fixture.debugElement.injector.get<SharedService>(SharedService as any);
    mockList = [];
    fixture.detectChanges();
  });

  it('should create department list component', () => {
    expect(component).toBeTruthy();
  });

  it('should call shared service when update department list', () => {
    const spy = spyOn(service, 'getDepartmentList').and.returnValue(of(mockList));
    component.updateDepartmentList();
    expect(spy.calls.any()).toBeTruthy();
  });
});
