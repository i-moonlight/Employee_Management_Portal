import { Component } from '@angular/core';
import { NotificationService } from '@services/notification.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'alert',
  templateUrl: 'alert.component.html',
  styleUrls: ['alert.component.scss']
})
export class AlertComponent {

  // 🚩-‍flag
  active: boolean;

  constructor(private notificationService: NotificationService) {}

  successMessage = this.notificationService.successMessageAction.pipe(
    tap((message) => {
      if (message) {
        setTimeout(() => {
          this.active = true
        }, 100)
        setTimeout(() => {
          this.active = false
        }, 20000);
        setTimeout(() => {
          this.notificationService.clearAllMessages();
        }, 22000);
      }
    })
  );

  errorMessage = this.notificationService.errorMessageAction.pipe(
    tap((message) => {
      if (message) {
        setTimeout(() => {
          this.active = true
        }, 100)
        setTimeout(() => {
          this.active = false
        }, 20000);
        setTimeout(() => {
          this.notificationService.clearAllMessages();
        }, 22000);
      }
    })
  );

  // public setCssClass(alert: Alert): string {
  //   if (!alert) return;
  //
  //   const classes = ['alert', 'alert-dismissable'];
  //
  //   const alertTypeClass = {
  //     [AlertType.Success]: 'alert-success',
  //     [AlertType.Error]: 'alert-danger',
  //     [AlertType.Info]: 'alert-info',
  //     [AlertType.Warning]: 'alert-warning'
  //   }
  //
  //   classes.push(alertTypeClass[alert.type]);
  //
  //   if (alert.fade) classes.push('fade');
  //
  //   return classes.join(' ');
  // }
}



