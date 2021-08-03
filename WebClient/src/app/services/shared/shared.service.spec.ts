import { inject, TestBed } from '@angular/core/testing';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { IDepartment } from '../../components/department/dep.comp';

describe('SharedService', () => {
  let service: SharedService;
  let formData: FormData;
  let mockDepartment: IDepartment;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule]
    });
    service = TestBed.inject(SharedService);
    formData = new FormData();
    mockDepartment = <IDepartment>{};
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
});
