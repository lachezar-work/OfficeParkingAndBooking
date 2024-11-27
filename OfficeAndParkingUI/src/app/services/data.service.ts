import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Employee, Car, GetOfficePresence, Room, AddOfficePresence } from '../models/models';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  constructor(private http: HttpClient) { 
  }

  private employees = new BehaviorSubject<Employee[]>([]);

  getEmployees(): Observable<Employee[]> {
    this.http.get<Employee[]>('/api/employee').subscribe(data => {
      this.employees.next(data);
    });
    return this.employees.asObservable();
  }
  
  private rooms = new BehaviorSubject<Room[]>([]);

  getRooms(): Observable<Room[]> {
    this.http.get<Room[]>('/api/room').subscribe(data => {
      this.rooms.next(data);
    });
    return this.rooms.asObservable();
  }

  private cars = new BehaviorSubject<Car[]>([
    { id: 1, employeeId: 1, brand: 'Toyota', registrationPlate: 'ABC123' },
    { id: 2, employeeId: 2, brand: 'Honda', registrationPlate: 'XYZ789' },
    { id: 3, employeeId: 3, brand: 'BMW', registrationPlate: 'DEF456' },
    { id: 4, employeeId: 4, brand: 'Audi', registrationPlate: 'GHI789' }
  ]);

  private presences = new BehaviorSubject<GetOfficePresence[]>([]);

  getPresences(): Observable<GetOfficePresence[]> {
    this.http.get<GetOfficePresence[]>('/api/officepresence').subscribe(data => {
      this.presences.next(data);
    });
    return this.presences.asObservable();
  }

  getCars(): Observable<Car[]> {
    return this.cars.asObservable();
  }

  addPresence(presence: AddOfficePresence): void {
    this.http.get<{ employeeName: string, employeeTeam: string }>('/api/employee/current')
      .pipe(
        tap(employee => {
          const current = this.presences.value;
          const newPresence: GetOfficePresence = {
            ...presence,
            id: current.length + 1,
            employeeName: employee.employeeName,
            employeeTeam: employee.employeeTeam
          };
          this.presences.next([...current, newPresence]);
        })
      ).subscribe();
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
