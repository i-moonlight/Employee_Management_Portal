import { Component } from '@angular/core';

export interface Todo {
  id: number;
  title: string;
  completed: boolean;
  date?: any;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  appTitle = 'Hello Angular';
}
