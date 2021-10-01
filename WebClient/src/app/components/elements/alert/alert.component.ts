import { Component } from '@angular/core';
import { NotificationService } from '@services/notification.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'alert',
  templateUrl: 'alert.component.html',
  styleUrls: ['alert.component.scss']
})
export class AlertComponent {
  active: boolean = false;

  constructor(private notificationService: NotificationService) {}

  successMessage = this.notificationService.successMessageAction.pipe(tap((message) => {
    if (message) {
      setTimeout(() => {
        this.active = true;
      }, 100);
      setTimeout(() => {
        this.active = false;
      }, 20000);
      setTimeout(() => {
        this.notificationService.clearAllMessages();
      }, 22000);
    }
  }));

  errorMessage = this.notificationService.errorMessageAction.pipe(tap((message) => {
    if (message) {
      setTimeout(() => {
        this.active = true;
      }, 100);
      setTimeout(() => {
        this.active = false;
      }, 20000);
      setTimeout(() => {
        this.notificationService.clearAllMessages();
      }, 22000);
    }
  }));

  public closeAlert(): void {
    setTimeout(() => {
      this.active = false;
    }, 100);
    setTimeout(() => {
      this.notificationService.clearAllMessages();
    }, 1000);
  }
}



