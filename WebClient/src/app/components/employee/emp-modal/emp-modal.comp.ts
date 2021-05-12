import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';

@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.comp.html',
  styleUrls: ['./emp-modal.comp.css']
})
export class EmployeeModalComponent implements OnInit {
  @Input() emp: any;
  employeeId: number;
  employeeName: string;
  department: string;
  dateOfJoining: string;
  photoFileName: string;
  photoFilePath: string;
  departmentsList: any = [];
  fileToUpload: File = null;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.loadDepartmentList()
  }

  private loadDepartmentList(): void {
    this.service.getAllDepartmentNames().subscribe((response: any) => {
      this.departmentsList = response;
      this.employeeId = this.emp.EmployeeId;
      this.employeeName = this.emp.EmployeeName;
      this.department = this.emp.Department;
      this.dateOfJoining = this.emp.DateOfJoining;
      this.photoFileName = this.emp.PhotoFileName;
      this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
    });
  }

  addEmployee(): void {
    let object = {
      EmployeeId: this.employeeId,
      EmployeeName: this.employeeName,
      Department: this.department,
      DateOfJoining: this.dateOfJoining,
      PhotoFileName: this.fileToUpload.name,
      PhotoFilePath: this.photoFilePath
    };
    this.service.addEmployee(object).subscribe(response => alert(response.toString()));
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
    this.service.updateEmployee(object).subscribe(response => alert(response.toString()));
    console.log(this.photoFileName, this.photoFilePath);
  }

  onFileSelected(event) {
    this.fileToUpload = event.target.files[0];

    // Show image preview.
    const reader = new FileReader();
    reader.onload = (event: any) => this.photoFilePath = event.target.result;
    reader.readAsDataURL(this.fileToUpload);
    console.log(this.photoFileName, this.photoFilePath, this.fileToUpload);
  }

  uploadPhoto(): void {
    const formData: FormData = new FormData();
    formData.append('File', this.fileToUpload, this.fileToUpload.name);

    this.service.uploadPhoto(formData).subscribe((response: string) => {
      try {
        this.photoFileName = response;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo is saved!')
      } catch (e) {
        e.console.error('Photo was not saved!')
      }
    });
  }

  updatePhoto(employeeId: number): void {
    const formData = new FormData();
    formData.append('File', this.fileToUpload, this.fileToUpload.name);
    this.service.updatePhoto(employeeId, formData).subscribe((data: string) => {
      try {
        this.photoFileName = data;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn('Photo is saved!')
      } catch (e) {
        e.console.error('Photo were not saved!')
      }
    });
  }
}
