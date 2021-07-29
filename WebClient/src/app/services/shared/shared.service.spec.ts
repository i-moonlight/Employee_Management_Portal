import { inject, TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { SharedService } from './shared.service';
import { Department } from '../../components/department/department.component';
import { Employee } from '../../components/employee/employee.component';

describe('SharedService', () => {
  let service: SharedService;
  let formData: FormData;
  let mockDepartment: Department;
  let mockEmployee: Employee;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule]
    });
    service = TestBed.inject(SharedService);
    formData = new FormData();
    mockDepartment = ({} as Department);
    mockEmployee = ({EmployeeId: 0} as Employee);
  });

  it('should create shared service', () => {
    expect(service).toBeTruthy();
  });

  it('should return department list from database', inject([SharedService, HttpTestingController],
    // tslint:disable-next-line:no-shadowed-variable
    (service: SharedService, backend: HttpTestingController) => {
      const mockDepartmentList: Department[] = [];
      service.getDepartmentListFromDB().subscribe((response: Department[]) =>
        expect(response).toEqual(mockDepartmentList));
      backend.expectOne({
        method: 'GET',
        url: 'http://localhost:8080/department'
      })
        .flush(mockDepartmentList);
    })
  );

  it('should return response when add department to database', inject([SharedService, HttpTestingController],
    // tslint:disable-next-line:no-shadowed-variable
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse = 'Create successful';
      service.addDepartmentToDB(mockDepartment).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'POST',
        url: 'http://localhost:8080/department'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when update department to database', inject([SharedService, HttpTestingController],
    // tslint:disable-next-line:no-shadowed-variable
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse = 'Update successful';
      service.updateDepartmentToDB(mockDepartment).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'PUT',
        url: 'http://localhost:8080/department'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when delete department to database', inject([SharedService, HttpTestingController],
    // tslint:disable-next-line:no-shadowed-variable
    (service: SharedService, backend: HttpTestingController) => {
      const mockDepartmentID: number = mockDepartment.DepartmentId;
      const mockResponse = 'Delete successful';
      service.deleteDepartmentFromDB(mockDepartmentID).subscribe((response: string) =>
          expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'DELETE',
        url: 'http://localhost:8080/department/' + mockDepartmentID
      })
        .flush(mockResponse);
    })
  );

  it('should return employee list from database', inject([SharedService, HttpTestingController],
    // tslint:disable-next-line:no-shadowed-variable
    (service: SharedService, backend: HttpTestingController) => {
      const mockEmployeeList: Employee[] = [];
      service.getEmployeeListFromDB().subscribe((response: Employee[]) =>
        expect(response).toEqual(mockEmployeeList));
      backend.expectOne({
        method: 'GET',
        url: 'http://localhost:8080/employee'
      })
        .flush(mockEmployeeList);
    })
  );
});
