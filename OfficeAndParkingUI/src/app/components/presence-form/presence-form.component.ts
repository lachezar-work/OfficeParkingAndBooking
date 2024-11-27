import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs/operators';
import { Room } from '../../models/models';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-presence-form',
  templateUrl: './presence-form.html',
  styles: [`
    .form-container {
      max-width: 600px;
      margin: 20px auto;
      padding: 20px;
    }
    mat-form-field {
      width: 100%;
      margin-bottom: 16px;
    }
    .presence-list {
      max-width: 800px;
      margin: 20px auto;
      padding: 20px;
    }
  `]
})
export class PresenceFormComponent implements OnInit {
  presenceForm: FormGroup;
  employees$;
  rooms: Room[] = [];
  parkingSpots = [1, 2, 3, 4];
  displayedColumns = ['employeeName', 'room', 'parkingSpot', 'notes'];
  
  // Declare a MatTableDataSource
  todayPresences: MatTableDataSource<any> = new MatTableDataSource();

  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
    private userService: UserService
  ) {
    this.employees$ = this.dataService.getEmployees();
    // Fetch today's presences and update the dataSource
    this.dataService.getPresences().pipe(
      map(presences => presences.filter(p => 
        new Date(p.date).toDateString() === new Date().toDateString()
      ))
    ).subscribe(presences => {
      this.todayPresences.data = presences; // Update the MatTableDataSource with filtered data
    });

    // Initialize the form
    this.presenceForm = this.fb.group({
      date: [new Date(), Validators.required],
      room: ['', Validators.required],
      needsParking: [false],
      parkingSpot: [''],
      parkingArrivalTime: [''],
      parkingDepartureTime: [''],
      notes: ['']
    });
  }
  ngOnInit(): void{
    this.dataService.getRooms().subscribe((rooms: Room[]) => {
      this.rooms = rooms;
    });

    this.userService.getUser().subscribe(username => {
      this.presenceForm.patchValue({ employeeName: username })
    }
    );
  }


  onSubmit() {
    if (this.presenceForm.valid) {
      const formValue = this.presenceForm.value;
      
      if (formValue.needsParking && !this.dataService.isParkingSpotAvailable(
        formValue.date,
        formValue.parkingSpot,
        formValue.parkingArrivalTime,
        formValue.parkingDepartureTime
      )) {
        alert('Parking spot not available for selected time period');
        return;
      }

      this.dataService.addPresence({
        date: formValue.date,
        roomNumber: formValue.roomId,
        parkingSpot: formValue.needsParking ? formValue.parkingSpot : undefined,
        parkingArrivalTime: formValue.needsParking ? formValue.parkingArrivalTime : undefined,
        parkingDepartureTime: formValue.needsParking ? formValue.parkingDepartureTime : undefined,
        notes: formValue.notes
      });

      this.presenceForm.reset({ date: new Date() });
    }
  }
}
