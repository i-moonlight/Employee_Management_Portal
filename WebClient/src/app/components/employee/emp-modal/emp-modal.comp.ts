import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { IEmployee } from '../emp.comp';

@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.comp.html',
  styleUrls: ['./emp-modal.comp.css']
})
export class EmployeeModalComponent implements OnInit {
  @Input() emp: IEmployee;
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

  private getEmployee(): IEmployee {
    return this.emp = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining,
      PhotoFileName: this.photoFileName
    };
  }

  addEmployee(): void {
    this.service.addEmployeeToDB(this.getEmployee()).subscribe((response: string) => {
      alert(response);
    });
  }

  updateEmployee(): void {
    this.service.updateEmployeeToDB(this.getEmployee()).subscribe(() => {
      console.warn(this.photoFileName, this.photoFilePath);
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
    this.service.uploadPhotoToStorage(this.formData).subscribe((response: string) => {
      try {
        this.photoFileName = response;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo upload')
      } catch (e) {
        e.console.error('Photo not upload')
      }
    });
  }

  updatePhoto(employeeId: number): void {
    this.service.updatePhotoToStorage(employeeId, this.formData).subscribe((response: string) => {
      try {
        this.photoFileName = response;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo update')
      } catch (e) {
        e.console.error('Photo not update')
      }
    });
  }
}
