import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DepartmentListComponent } from './dep-list.comp';

describe('DepartmentListComponent', () => {
  let component: DepartmentListComponent;
  let fixture: ComponentFixture<DepartmentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentListComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create department list component', () => {
    expect(component).toBeTruthy();
  });
});
