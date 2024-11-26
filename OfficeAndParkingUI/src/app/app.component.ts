import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styles: [`
    mat-toolbar {
      margin-bottom: 20px;
    }
    mat-toolbar a {
      margin-left: 16px;
    }
  `]
})
export class AppComponent {
  isLoggedIn = false;
  fullName = 'John Doe';

  logout() {
    // Implement logout logic here
    this.isLoggedIn = false;
  }
}
