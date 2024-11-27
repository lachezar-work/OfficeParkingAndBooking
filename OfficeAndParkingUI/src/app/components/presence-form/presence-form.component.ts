import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Employee, GetOfficePresence, AddOfficePresence, Room } from '../../models/models';
import { UserService } from '../user/user.service';
import { Table } from 'primeng/table';
import { SortEvent } from 'primeng/api';

@Component({
  selector: 'app-presence-form',
  templateUrl: './presence-form.html',
  styles: [`
.form-container {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.form-group-checkbox {
  display: flex;
  align-items: center;
  gap: 10px;
}

.input-field {
  width: 100%;
}

button[pButton] {
  align-self: flex-end;
  margin-top: 20px;
}

button[pButton] {
  display: block;
  width: 100%;
  margin-top: 20px;
}
    .presence-list {
      max-width: 800px;
      margin: 20px auto;
      padding: 20px;
    }
  `]
})
export class PresenceFormComponent implements OnInit {
  @ViewChild('dt') dt!: Table;
  selectedEmployee: string | undefined;
  isSorted: boolean | null = null;
  searchValue: string | undefined;
  presenceForm: FormGroup;
  employees: Employee[]=[];
  rooms: Room[] = [];
  parkingSpots = [1, 2, 3, 4];
  
  allPresences: GetOfficePresence[] = [];


  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
    private userService: UserService
  ) {


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
  ngOnInit(): void{
    this.dataService.getRooms().subscribe((rooms: Room[]) => {
      this.rooms = rooms;
    });
    this.dataService.getEmployees().subscribe((employees: Employee[]) => {
      this.employees = employees;
    });
    this.userService.getUser().subscribe(username => {
      this.presenceForm.patchValue({ employeeName: username })
    });
    this.dataService.getPresences()
    .subscribe(presences => {
      this.allPresences = presences; 
    });
  }
  getTeamNameOfEmployee(employeeId: number){
    return this.employees.find(e => e.id === employeeId)?.teamName;
  }
  customSort(event: SortEvent) {
    if (this.isSorted == null || this.isSorted === undefined) {
        this.isSorted = true;
        this.sortTableData(event);
    } else if (this.isSorted == true) {
        this.isSorted = false;
        this.sortTableData(event);
    } else if (this.isSorted == false) {
        this.isSorted = null;
        this.allPresences = [...this.allPresences];
        this.dt.reset();
    }
}
clear(table: Table) {
  table.clear();
  this.searchValue = ''
}
applyFilterGlobal($event: any, stringVal: any) {
  this.dt!.filterGlobal(($event.target as HTMLInputElement).value, stringVal);
}
sortTableData(event: any) {
  event.data.sort((data1: any, data2: any) => {
      let value1 = data1[event.field];
      let value2 = data2[event.field];
      let result = null;
      if (value1 == null && value2 != null) result = -1;
      else if (value1 != null && value2 == null) result = 1;
      else if (value1 == null && value2 == null) result = 0;
      else if (typeof value1 === 'string' && typeof value2 === 'string') result = value1.localeCompare(value2);
      else result = value1 < value2 ? -1 : value1 > value2 ? 1 : 0;

      return event.order * result;
  });
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
        employeeId: formValue.employeeId,
        parkingSpot: formValue.needsParking ? formValue.parkingSpot : undefined,
        parkingArrivalTime: formValue.needsParking ? formValue.parkingArrivalTime : undefined,
        parkingDepartureTime: formValue.needsParking ? formValue.parkingDepartureTime : undefined,
        notes: formValue.notes
      });

      this.presenceForm.reset({ date: new Date() });
    }
    else {
      // If form is invalid, display error messages
      alert('Please fill out all required fields correctly.');
    }
  }
}
