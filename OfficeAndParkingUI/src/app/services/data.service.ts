import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Employee, Car, OfficePresence, Team, Room } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private employees = new BehaviorSubject<Employee[]>([
    { id: 1, name: 'John Smith', team: Team.Java },
    { id: 2, name: 'Emma Wilson', team: Team.BA },
    { id: 3, name: 'Michael Brown', team: Team.DevOps },
    { id: 4, name: 'Sarah Davis', team: Team.HR },
    { id: 5, name: 'James Johnson', team: Team.DotNet }
  ]);

  private cars = new BehaviorSubject<Car[]>([
    { id: 1, employeeId: 1, brand: 'Toyota', registrationPlate: 'ABC123' },
    { id: 2, employeeId: 2, brand: 'Honda', registrationPlate: 'XYZ789' },
    { id: 3, employeeId: 3, brand: 'BMW', registrationPlate: 'DEF456' },
    { id: 4, employeeId: 4, brand: 'Audi', registrationPlate: 'GHI789' }
  ]);

  private presences = new BehaviorSubject<OfficePresence[]>([
    {
      id: 1,
      date: new Date('2024-01-15'),
      employeeId: 1,
      room: Room.Room403,
      parkingSpot: 1,
      parkingArrivalTime: '09:00',
      parkingDepartureTime: '17:00',
      notes: 'Team meeting in the morning'
    },
    {
      id: 2,
      date: new Date('2024-01-15'),
      employeeId: 2,
      room: Room.Room404,
      parkingSpot: 2,
      parkingArrivalTime: '08:30',
      parkingDepartureTime: '16:30',
      notes: 'Client presentation'
    },
    {
      id: 3,
      date: new Date('2024-01-16'),
      employeeId: 3,
      room: Room.Room403,
      notes: 'Working on deployment scripts'
    },
    {
      id: 4,
      date: new Date('2024-01-16'),
      employeeId: 4,
      room: Room.Room404,
      parkingSpot: 3,
      parkingArrivalTime: '09:30',
      parkingDepartureTime: '18:00',
      notes: 'HR interviews'
    }
  ]);

  getEmployees(): Observable<Employee[]> {
    return this.employees.asObservable();
  }

  getCars(): Observable<Car[]> {
    return this.cars.asObservable();
  }

  getPresences(): Observable<OfficePresence[]> {
    return this.presences.asObservable();
  }

  addPresence(presence: OfficePresence) {
    const current = this.presences.value;
    this.presences.next([...current, { ...presence, id: current.length + 1 }]);
  }

  addCar(car: Car) {
    const current = this.cars.value;
    this.cars.next([...current, { ...car, id: current.length + 1 }]);
  }

  isParkingSpotAvailable(date: Date, spot: number, arrivalTime: string, departureTime: string): boolean {
    return !this.presences.value.some(p => 
      p.parkingSpot === spot && 
      new Date(p.date).toDateString() === new Date(date).toDateString() &&
      this.isTimeOverlapping(p.parkingArrivalTime!, p.parkingDepartureTime!, arrivalTime, departureTime)
    );
  }

  private isTimeOverlapping(start1: string, end1: string, start2: string, end2: string): boolean {
    return start1 <= end2 && start2 <= end1;
  }
}