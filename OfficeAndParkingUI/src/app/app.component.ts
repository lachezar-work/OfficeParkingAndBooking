import { Component, OnInit } from '@angular/core';
import { UserService } from './components/user/user.service';
import { IUser } from './components/user/user.model';

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
export class AppComponent implements OnInit {
  public isLoggedIn = false;
  public user: IUser | null = null;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.userService.checkLoginStatus().subscribe(user => {
      this.isLoggedIn = user !== null;
      this.user = user;
    });
  }

  logout() {
    this.userService.signOut().subscribe({
      next: () => {
        this.isLoggedIn = false;
        this.user = null;
      },
      error: err => {
        console.error('Logout failed', err);
      }
    });
  }
}
