import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Employee, Car, GetOfficePresence, Room, AddOfficePresence, ParkingSpot, Team } from '../models/models';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  constructor(private http: HttpClient, private datePipe: DatePipe) { 
    this.getEmployees().subscribe();
  }

  private employees = new BehaviorSubject<Employee[]>([]);
  private presences = new BehaviorSubject<GetOfficePresence[]>([]);
  private rooms = new BehaviorSubject<Room[]>([]);
  private parkingSpots = new BehaviorSubject<ParkingSpot[]>([]);
  private cars = new BehaviorSubject<Car[]>([]);
  private teams = new BehaviorSubject<Team[]>([]);

  getEmployees(): Observable<Employee[]> {
    this.http.get<Employee[]>('/api/employee').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.employees.next(data);
    });
    return this.employees.asObservable();
  }
  getParkingSpots(): Observable<ParkingSpot[]> {
    this.http.get<ParkingSpot[]>('/api/parkingspot').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.parkingSpots.next(data);
    });
    return this.parkingSpots.asObservable();
  }
  getRooms(): Observable<Room[]> {
    this.http.get<Room[]>('/api/room').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.rooms.next(data);
    });
    return this.rooms.asObservable();
  }
  getPresences(): Observable<GetOfficePresence[]> {
    this.http.get<GetOfficePresence[]>('/api/officepresence').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.presences.next(data);
    });
    return this.presences.asObservable();
  }
  getCars(): Observable<Car[]> {
    this.http.get<Car[]>('/api/car').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.cars.next(data);
    });
    return this.cars.asObservable();
  }
  getTeams(): Observable<Team[]> {
    this.http.get<Team[]>('/api/team').pipe(
      catchError(this.handleError)
    ).subscribe(data => {
      this.teams.next(data);
    });
    return this.teams.asObservable();
  }

  addPresence(presence: AddOfficePresence): Observable<GetOfficePresence> {
    return this.http.post<GetOfficePresence>('/api/officepresence/add', presence)
      .pipe(
        tap((response: GetOfficePresence) => {
          const current = this.presences.value;
          const newPresence: GetOfficePresence = {
            ...presence,
            id: current.length + 1,
            roomNumber: this.rooms.value.find(r => r.id === presence.roomId)!.number,
            employeeName: response.employeeName,
            teamName: response.teamName
          };
          this.presences.next([...current, newPresence]);
          return newPresence;
        }),
        catchError(this.handleError)
      );
  }

  addCar(car: Car): Observable<Car> {
    return this.http.post<Car>('/api/car/add', car).pipe(
      tap((newCar: Car) => {
        const current = this.cars.value;
        this.cars.next([...current, newCar]);
      }),
      catchError(this.handleError)
    );
  }

  public getEmployeeName(employeeId: number): string {
    const employee = this.employees.value.find(e => e.id === employeeId);
    return employee ? employee.fullName : 'Unknown';
  }

  public  convertToTimeOnly(dateTimeString: string): string {
    const date = new Date(dateTimeString);
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
  }
  isParkingSpotAvailable(date: string, spot: number, arrivalTime: string, departureTime: string): boolean {
    return !this.presences.value.some(p => 
      p.parkingSpot === spot && 
      new Date(p.date).toDateString() === new Date(date).toDateString() &&
      this.isTimeOverlapping(p.parkingArrivalTime!, p.parkingDepartureTime!, arrivalTime, departureTime)
    );
  }

  private isTimeOverlapping(start1: string, end1: string, start2: string, end2: string): boolean {
    const [start1Hour, start1Minute] = start1.split(':').map(Number);
    const [end1Hour, end1Minute] = end1.split(':').map(Number);
    const [start2Hour, start2Minute] = start2.split(':').map(Number);
    const [end2Hour, end2Minute] = end2.split(':').map(Number);

    const start1Time = new Date(0, 0, 0, start1Hour, start1Minute);
    const end1Time = new Date(0, 0, 0, end1Hour, end1Minute);
    const start2Time = new Date(0, 0, 0, start2Hour, start2Minute);
    const end2Time = new Date(0, 0, 0, end2Hour, end2Minute);

    return (start1Time < end2Time && start2Time < end1Time);
  }

  private handleError(error: any) {
    console.error('An error occurred:', error.error.message);
    return throwError(() => new Error(error.error.message || 'Server error'));
  }
}
