import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IUserCredentials } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent {
  credentials: IUserCredentials = { username: '', password: '' };
  signInError: boolean = false;
  signInErrorMessage: string = "";

  constructor(private userService: UserService, private router: Router) { }

  signIn() {
    this.signInError = false;
    this.userService.signIn(this.credentials).subscribe({
      next: () => {
        this.router.navigate(['/presence']);
      },
      error: err => {
        this.signInError = true;
        this.signInErrorMessage = err.error?.message || 'An unexpected error occurred.'
      }
    });
  }

}
