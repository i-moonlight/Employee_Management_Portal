import { Component, OnInit } from '@angular/core';
import { NgProgress } from '@ngx-progressbar/core';
import { ProgressBarService } from '@services/progress-bar/progress-bar.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {

  constructor(private progress: NgProgress, public progressBar: ProgressBarService) {}

  ngOnInit() {
    this.progressBar.progressRef = this.progress.ref('progressBar');
  }
}
