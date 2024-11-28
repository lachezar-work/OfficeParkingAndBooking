import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { Employee, GetOfficePresence, Room ,ParkingSpot, Car} from '../../models/models';
import { UserService } from '../user/user.service';
import { Table } from 'primeng/table';
import { SortEvent } from 'primeng/api';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-presence-form',
  templateUrl: './presence-form.html'
})
export class PresenceFormComponent implements OnInit {
  @ViewChild('dt') dt!: Table;
  selectedEmployee: string | undefined;
  isSorted: boolean | null = null;
  searchValue: string | undefined;
  presenceForm: FormGroup;

  employees: Employee[]=[];
  rooms: Room[] = [];
  parkingSpots: ParkingSpot[]= [];
  allCars: Car[] = [];
  filteredCars: Car[] = [];
  allPresences: GetOfficePresence[] = [];

  constructor(
    private fb: FormBuilder,
    private dataService: DataService,
    private userService: UserService,
    private datePipe: DatePipe
  ) {

    // Initialize the form
    this.presenceForm = this.fb.group({
      date: [new Date(), Validators.required],
      roomId: ['', Validators.required],
      employeeId: ['', Validators.required],
      needsParking: [false],
      car: [{ value: '', disabled: true }],
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
    this.dataService.getCars().subscribe((cars: Car[]) => {
      this.allCars = cars;
    });
    this.dataService.getParkingSpots().subscribe((parkingSpots: ParkingSpot[]) => {
      this.parkingSpots = parkingSpots;
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

    this.presenceForm.get('employeeId')?.valueChanges.subscribe(employeeId => {
      if (employeeId) {
        this.filteredCars = this.allCars.filter(car => car.employeeId === employeeId);
        this.presenceForm.get('car')?.enable();
      } else {
        this.filteredCars = [];
        this.presenceForm.get('car')?.disable();
      }
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
        roomId: formValue.roomId,
        carId: formValue.car,
        employeeId: formValue.employeeId,
        parkingSpot: formValue.needsParking ? formValue.parkingSpot : undefined,
        parkingArrivalTime: formValue.needsParking ? this.dataService.convertToTimeOnly(formValue.parkingArrivalTime) : undefined,
        parkingDepartureTime: formValue.needsParking ? this.dataService.convertToTimeOnly(formValue.parkingDepartureTime) : undefined,
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
