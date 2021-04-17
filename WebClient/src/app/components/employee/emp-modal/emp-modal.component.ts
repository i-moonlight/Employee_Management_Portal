import {Component, Input, OnInit} from '@angular/core';
import {SharedService} from '../../../services/shared/shared.service';
import {Employee} from '../employee.component';

@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.component.html',
  styleUrls: ['./emp-modal.component.css']
})
export class EmployeeModalComponent implements OnInit {
  @Input() emp: Employee;
  departmentList: string[];
  employeeId: number;
  employeeName: string;
  department: string;
  dateOfJoining: string;
  photoFileName: string;
  photoFilePath: string;
  fileToUpload: File;
  formData: FormData;

  constructor(private service: SharedService) {
    this.fileToUpload = null;
    this.formData = new FormData();
  }

  ngOnInit(): void {
    this.loadDepartmentList()
    this.employeeId = this.emp.EmployeeId;
    this.employeeName = this.emp.EmployeeName;
    this.department = this.emp.Department;
    this.dateOfJoining = this.emp.DateOfJoining;
    this.photoFileName = this.emp.PhotoFileName;
    this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
  }

  loadDepartmentList(): void {
    this.service.getAllDepartmentNamesFromDB().subscribe((response: string[]) => {
      this.departmentList = response;
    });
  }

  private getEmployee(): Employee {
    return this.emp = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining,
      PhotoFileName: this.photoFileName
    }
  }

  updateEmployee(): void {
    let object = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining,
      PhotoFileName: this.photoFileName,
      PhotoFilePath: this.photoFilePath
    };
    this.service.updateEmployeeToDB(object).subscribe(res => alert(res.toString()));
    console.log(this.photoFileName, this.photoFilePath);
  }

  addEmployee(): void {
    this.service.addEmployeeToDB(this.getEmployee()).subscribe((response: string) => {
      alert(response);
    });
  }


  onFileSelected(event: any): void {
    this.fileToUpload = event.target.files[0];
    this.formData.append('File', this.fileToUpload, this.fileToUpload.name);

    // Show image preview.
    const reader = new FileReader();
    reader.onload = (event: any) => this.photoFilePath = event.target.result;
    reader.readAsDataURL(this.fileToUpload);
    console.log(this.photoFileName, this.photoFilePath, this.fileToUpload);
  }

  uploadPhoto(): void {
    const formData: FormData = new FormData();
    formData.append('File', this.fileToUpload, this.fileToUpload.name);

    this.service.uploadPhotoToStorage(formData).subscribe((res: string) => {
      try {
        this.photoFileName = res;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo is saved!')
      } catch (e) {
        e.console.error('Photo was not saved!')
      }
    })
  }

  updatePhoto(employeeId: number): void {
    this.service.updatePhotoToStorage(employeeId, this.formData).subscribe((res: string) => {
      try {
        this.photoFileName = res;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo update')
      } catch (e) {
        e.console.error('Photo not update')
      }
    });
  }
}
