import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  protected successMessageSubject = new Subject<string>();
  successMessageAction = this.successMessageSubject.asObservable();

  protected errorMessageSubject = new Subject<string>();
  errorMessageAction = this.errorMessageSubject.asObservable();

  setSuccessMessage(message: string): void {
    this.successMessageSubject.next(message);
  }

  setErrorMessage(message: string): void {
    this.errorMessageSubject.next(message);
  }

  clearSuccessMessage(): void {
    this.setSuccessMessage('');
  }

  clearErrorMessage(): void {
    this.setErrorMessage('');
  }

  clearAllMessages(): void {
    this.clearSuccessMessage();
    this.clearErrorMessage();
  }
}
