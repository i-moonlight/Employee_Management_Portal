import { inject, TestBed } from '@angular/core/testing';
import { SharedService } from './shared.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { IDepartment } from '../../components/department/dep.comp';

describe('SharedService', () => {
  let service: SharedService;
  let formData: FormData;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, HttpClientTestingModule]
    });
    service = TestBed.inject(SharedService);
    formData = new FormData();
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
});
