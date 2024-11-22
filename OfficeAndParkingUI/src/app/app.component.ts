import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <mat-toolbar color="primary">
      <span>Office Management System</span>
      <a mat-button routerLink="/presence">Office Presence</a>
      <a mat-button routerLink="/cars">Car Management</a>
    </mat-toolbar>

    <router-outlet></router-outlet>
  `,
  styles: [`
    mat-toolbar {
      margin-bottom: 20px;
    }
    mat-toolbar a {
      margin-left: 16px;
    }
  `]
})
export class AppComponent {}