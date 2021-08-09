import { inject, TestBed } from '@angular/core/testing';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { IDepartment } from '../../components/department/dep.comp';
import { IEmployee } from '../../components/employee/emp.comp';

describe('SharedService', () => {
  let service: SharedService;
  let formData: FormData;
  let mockDepartment: IDepartment;
  let mockEmployee: IEmployee;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule]
    });
    service = TestBed.inject(SharedService);
    formData = new FormData();
    mockDepartment = <IDepartment>{};
    mockEmployee = <IEmployee>{EmployeeId: 0};
  });

  it('should create shared service', () => {
    expect(service).toBeTruthy();
  });

  it('should return department list from database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockDepartmentList: IDepartment[] = [];
      service.getDepartmentListFromDB().subscribe((response: IDepartment[]) =>
        expect(response).toEqual(mockDepartmentList));
      backend.expectOne({
        method: 'GET',
        url: 'http://localhost:5000/api/department'
      })
        .flush(mockDepartmentList);
    })
  );

  it('should return response when add department to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'Create successful';
      service.addDepartmentToDB(mockDepartment).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'POST',
        url: 'http://localhost:5000/api/department'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when update department to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'Update successful';
      service.updateDepartmentToDB(mockDepartment).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'PUT',
        url: 'http://localhost:5000/api/department'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when delete department to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockDepartmentID: number = mockDepartment.DepartmentId;
      const mockResponse: string = 'Delete successful';
      service.deleteDepartmentFromDB(mockDepartmentID).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'DELETE',
        url: 'http://localhost:5000/api/department/' + mockDepartmentID
      })
        .flush(mockResponse);
    })
  );

  it('should return employee list from database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockEmployeeList: IEmployee[] = [];
      service.getEmployeeListFromDB().subscribe((response: IEmployee[]) =>
        expect(response).toEqual(mockEmployeeList));
      backend.expectOne({
        method: 'GET',
        url: 'http://localhost:5000/api/employee'
      })
        .flush(mockEmployeeList);
    })
  );

  it('should return response when add employee to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'Create successful';
      service.addEmployeeToDB(mockEmployee).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'POST',
        url: 'http://localhost:5000/api/employee/'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when update employee to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'Update successful';
      service.updateEmployeeToDB(mockEmployee).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'PUT',
        url: 'http://localhost:5000/api/employee'
      })
        .flush(mockResponse);
    })
  );

  it('should return response when delete employee to database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockEmployeeID: number = mockEmployee.EmployeeId;
      const mockResponse: string = 'Delete successful';
      service.deleteEmployeeFromDB(mockEmployeeID).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'DELETE',
        url: 'http://localhost:5000/api/employee/' + mockEmployeeID
      })
        .flush(mockResponse);
    })
  );

  it('should return upload photo file name from database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'photo file name';
      service.uploadPhotoToStorage(formData).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'POST',
        url: 'http://localhost:5000/api/employee/UploadPhoto/'
      })
        .flush(mockResponse);
    })
  );

  it('should return update photo file name from database', inject([SharedService, HttpTestingController],
    (service: SharedService, backend: HttpTestingController) => {
      const mockResponse: string = 'photo file name';
      service.updatePhotoToStorage(mockEmployee.EmployeeId, formData).subscribe((response: string) =>
        expect(response).toEqual(mockResponse));
      backend.expectOne({
        method: 'POST',
        url: 'http://localhost:5000/api/employee/0/UpdatePhoto'
      })
        .flush(mockResponse);
    })
  );
});
