import { Component } from '@angular/core';

export interface Todo {
  id: number;
  title: string;
  completed: boolean;
  date?: any;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.comp.html',
  styleUrls: ['./app.comp.css']
})

export class AppComponent {
  appTitle = 'Hello Angular';
}
