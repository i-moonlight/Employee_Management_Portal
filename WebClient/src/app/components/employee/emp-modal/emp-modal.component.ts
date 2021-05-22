import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from '../../../services/shared/shared.service';
import { Employee } from '../employee.component';

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
  buttonClicked: boolean;

  constructor(private service: SharedService) {}

  ngOnInit(): void {
    this.employeeId = this.emp.EmployeeId;
    this.employeeName = this.emp.EmployeeName;
    this.department = this.emp.Department;
    this.dateOfJoining = this.emp.DateOfJoining;
    this.photoFileName = this.emp.PhotoFileName;
    this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
    this.fileToUpload = null;
    this.formData = new FormData();
    this.buttonClicked = false;
    this.loadDepartmentList();
  }

  loadDepartmentList(): void {
    this.service.getAllDepartmentNamesFromDB().subscribe((res: string[]) =>
      this.departmentList = res);
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

  addEmployee(): void {
    this.service.addEmployeeToDB(this.getEmployee()).subscribe((res: string) =>
      alert(res));
  }

  updateEmployee(): void {
    this.service.updateEmployeeToDB(this.getEmployee()).subscribe(() =>
      console.warn(this.photoFilePath));
  }

  onFileSelected(event: any): void {
    this.fileToUpload = event.target.files[0];
    this.formData.append('File', this.fileToUpload, this.fileToUpload.name);

    // Show image preview
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

  uploadEmployeeData(): void {
    this.service.uploadPhotoToStorage(this.formData).subscribe((res: string) => {
        this.photoFileName = res;
        this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
        console.warn(res)
      },
      (error: string) => console.error(error)
    );
  }

  updateEmployeeData(employeeId: number): void {
    this.service.updatePhotoToStorage(employeeId, this.formData).subscribe((res: string) => {
        if (res == 'anonymous.png') {
          this.photoFileName = this.emp.PhotoFileName;
          this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
          this.updateEmployee();
        } else {
          this.photoFileName = res;
          this.photoFilePath = this.service.PhotoUrl + this.photoFileName;
          this.updateEmployee()
        }
        console.warn(res)
      },
      (error: string) => console.error(error)
    );
  }
}
