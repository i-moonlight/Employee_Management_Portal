import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DepartmentComponent } from './dep.comp';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('DepartmentComponent', () => {
  let component: DepartmentComponent;
  let fixture: ComponentFixture<DepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create department component', () => {
    expect(component).toBeTruthy();
  });
});
