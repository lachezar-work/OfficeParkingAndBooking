import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Team, Room } from '../../models/models';
import { MatTableDataSource } from '@angular/material/table';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-presence-form',
  template: `
     <form [formGroup]="presenceForm" (ngSubmit)="onSubmit()" class="form-container">
      <h2>Office Presence Registration</h2>
      
      <mat-form-field>
        <mat-label>Date</mat-label>
        <input matInput [matDatepicker]="picker" formControlName="date">
        <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
        <mat-datepicker #picker></mat-datepicker>
      </mat-form-field>

      <mat-form-field>
        <mat-label>Room</mat-label>
        <mat-select formControlName="room">
          <mat-option *ngFor="let room of rooms" [value]="room">
            {{room}}
          </mat-option>
        </mat-select>
      </mat-form-field>

      <mat-form-field>
        <mat-label>Employee</mat-label>
        <mat-select formControlName="employeeId">
          <mat-option *ngFor="let employee of employees$ | async" [value]="employee.id">
            {{employee.name}} ({{employee.team}})
          </mat-option>
        </mat-select>
      </mat-form-field>

      <mat-checkbox formControlName="needsParking">Need Parking Spot</mat-checkbox>

      <div *ngIf="presenceForm.get('needsParking')?.value">
        <mat-form-field>
          <mat-label>Parking Spot</mat-label>
          <mat-select formControlName="parkingSpot">
            <mat-option *ngFor="let spot of parkingSpots" [value]="spot">
              Spot {{spot}}
            </mat-option>
          </mat-select>
        </mat-form-field>

        <mat-form-field>
          <mat-label>Arrival Time</mat-label>
          <input matInput type="time" formControlName="parkingArrivalTime">
        </mat-form-field>

        <mat-form-field>
          <mat-label>Departure Time</mat-label>
          <input matInput type="time" formControlName="parkingDepartureTime">
        </mat-form-field>
      </div>

      <mat-form-field>
        <mat-label>Notes</mat-label>
        <textarea matInput formControlName="notes"></textarea>
      </mat-form-field>

      <button mat-raised-button color="primary" type="submit" [disabled]="!presenceForm.valid">
        Submit
      </button>
    </form>

    <div class="presence-list">
      <h3>Today's Office Presence</h3>
      <mat-table [dataSource]="todayPresences">
        <ng-container matColumnDef="employeeName">
          <mat-header-cell *matHeaderCellDef> Employee </mat-header-cell>
          <mat-cell *matCellDef="let presence"> {{getEmployeeName(presence.employeeId)}} </mat-cell>
        </ng-container>

        <ng-container matColumnDef="room">
          <mat-header-cell *matHeaderCellDef> Room </mat-header-cell>
          <mat-cell *matCellDef="let presence"> {{presence.room}} </mat-cell>
        </ng-container>

        <ng-container matColumnDef="parkingSpot">
          <mat-header-cell *matHeaderCellDef> Parking </mat-header-cell>
          <mat-cell *matCellDef="let presence"> 
            {{presence.parkingSpot ? 'Spot ' + presence.parkingSpot : 'No parking'}}
          </mat-cell>
        </ng-container>

        <ng-container matColumnDef="notes">
          <mat-header-cell *matHeaderCellDef> Notes </mat-header-cell>
          <mat-cell *matCellDef="let presence"> {{presence.notes}} </mat-cell>
        </ng-container>

        <mat-header-row *matHeaderRowDef="displayedColumns"></mat-header-row>
        <mat-row *matRowDef="let row; columns: displayedColumns;"></mat-row>
      </mat-table>
    </div>
    
  `,
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
export class PresenceFormComponent {
  presenceForm: FormGroup;
  employees$;
  rooms = Object.values(Room);
  parkingSpots = [1, 2, 3, 4];
  displayedColumns = ['employeeName', 'room', 'parkingSpot', 'notes'];
  
  // Declare a MatTableDataSource
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  todayPresences: MatTableDataSource<any> = new MatTableDataSource();

  constructor(
    private fb: FormBuilder,
    private dataService: DataService
  ) {
    this.employees$ = this.dataService.getEmployees();
    // Fetch today's presences and update the dataSource
    this.dataService.getPresences().pipe(
      map(presences => presences.filter(p => 
        new Date(p.date).toDateString() === new Date().toDateString()
      ))
    ).subscribe(presences => {
      this.dataSource.data = presences; // Update the MatTableDataSource with filtered data
    });

    // Initialize the form
    this.presenceForm = this.fb.group({
      date: [new Date(), Validators.required],
      room: ['', Validators.required],
      employeeId: ['', Validators.required],
      needsParking: [false],
      parkingSpot: [''],
      parkingArrivalTime: [''],
      parkingDepartureTime: [''],
      notes: ['']
    });
  }

  getEmployeeName(employeeId: number): string {
    const employees = (this.dataService.getEmployees() as any).value;
    const employee = employees.find((e: any) => e.id === employeeId);
    return employee ? employee.name : 'Unknown';
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
        id: 0,
        date: formValue.date,
        employeeId: formValue.employeeId,
        room: formValue.room,
        parkingSpot: formValue.needsParking ? formValue.parkingSpot : undefined,
        parkingArrivalTime: formValue.needsParking ? formValue.parkingArrivalTime : undefined,
        parkingDepartureTime: formValue.needsParking ? formValue.parkingDepartureTime : undefined,
        notes: formValue.notes
      });

      this.presenceForm.reset({ date: new Date() });
    }
  }
}
