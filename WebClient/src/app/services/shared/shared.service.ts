import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../../components/employee/employee.component';
import { Department } from '../../components/department/department.component';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl = 'http://localhost:8080';
  readonly PhotoUrl = 'http://localhost:8080/Photos/';

  constructor(private http: HttpClient) {}

  getDepartmentListFromDB(): Observable<Department[]> {
    return this.http.get<Department[]>(this.APIUrl + '/department');
  }

  addDepartmentToDB(department: Department): Observable<string> {
    return this.http.post<string>(this.APIUrl + '/department', department);
  }

  updateDepartmentToDB(department: Department): Observable<string> {
    return this.http.put<string>(this.APIUrl + '/department', department);
  }

  deleteDepartmentFromDB(departmentId: number): Observable<string> {
    return this.http.delete<string>(this.APIUrl + '/department/' + departmentId);
  }

  getEmployeeListFromDB(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.APIUrl + '/employee');
  }

  getAllDepartmentNamesFromDB(): Observable<string[]> {
    return this.http.get<string[]>(this.APIUrl + '/employee/GetAllDepartmentNames');
  }

  addEmployeeToDB(employee: Employee): Observable<string> {
    return this.http.post<string>(this.APIUrl + '/employee/', employee);
  }

  updateEmployeeToDB(employee: Employee): Observable<string> {
    return this.http.put<string>(this.APIUrl + '/employee', employee);
  }

  deleteEmployeeFromDB(employeeId: number): Observable<string> {
    return this.http.delete<string>(this.APIUrl + '/employee/' + employeeId);
  }

  uploadPhotoToStorage(formData: FormData): Observable<string> {
    return this.http.post<string>(this.APIUrl + '/employee/UploadPhoto/', formData);
  }

  updatePhotoToStorage(employeeId: number, formData: FormData): Observable<string> {
    return this.http.post<string>(this.APIUrl + '/employee/' + employeeId + '/UpdatePhoto', formData);
  }
}
