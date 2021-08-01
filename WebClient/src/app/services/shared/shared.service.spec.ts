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
      const mockDepartment: IDepartment = <IDepartment>{};
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
});
