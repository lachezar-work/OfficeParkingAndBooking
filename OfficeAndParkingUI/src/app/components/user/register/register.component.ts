import { Component } from '@angular/core';
import { UserService } from '../user.service';
import { RegisterModel } from '../../../models/register.model';
import { Team } from '../../../models/models';
import { DataService } from '../../../services/data.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: RegisterModel = {
    firstName: '',
    lastName: '',
    teamId: 0,
    username: '',
    password: ''
  };
  registerError: boolean = false;
  registerErrorMessage: string = '';
  registerSuccess: boolean = false;
  teams: Team[] = [];

  constructor(private userService: UserService, private dataService: DataService) {
    this.dataService.getTeams().subscribe((teams: Team[]) => {
      this.teams = teams;
    });
  }

  register() {
    this.userService.register(this.registerData).subscribe({
      next: () => {
        this.registerSuccess = true;
        this.registerError = false;
      },
      error: (err) => {
        this.registerError = true;
        if (err.error && Array.isArray(err.error) && err.error.length > 0) {
          this.registerErrorMessage = err.error[0].description || 'Registration failed';
        } else if (err.error && err.error.errors) {
          const errorMessages = Object.values(err.error.errors).flat(1);
          this.registerErrorMessage = errorMessages.join('<br>') || 'Registration failed';
        } else {
          this.registerErrorMessage = err;
        }
        this.registerSuccess = false;
      }
    });
  }
}