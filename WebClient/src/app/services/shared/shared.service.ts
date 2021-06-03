import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployee } from '../../components/employee/emp.comp';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl = 'http://localhost:5000/api';
  readonly PhotoUrl = 'http://localhost:5000/Photos/';

  constructor(private http: HttpClient) {}

  getDepartmentList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/department');
  }

  addDepartment(val: any): Observable<any> {
    return this.http.post(this.APIUrl + '/department', val);
  }

  updateDepartment(val: any): Observable<any> {
    return this.http.put(this.APIUrl + '/department', val);
  }

  deleteDepartment(val: any): Observable<any> {
    return this.http.delete(this.APIUrl + '/department/' + val);
  }

  getEmployeeListFromDB(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(this.APIUrl + '/employee');
  }

  getAllDepartmentNamesFromDB(): Observable<string[]> {
    return this.http.get<string[]>(this.APIUrl + '/employee/GetAllDepartmentNames');
  }

  addEmployeeToDB(employee: IEmployee): Observable<string> {
    return this.http.post<string>(this.APIUrl + '/employee/', employee);
  }

  updateEmployeeToDB(employee: IEmployee): Observable<string> {
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
