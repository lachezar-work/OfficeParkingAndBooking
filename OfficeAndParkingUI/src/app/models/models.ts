import { Time } from "@angular/common";

export interface Employee {
  id: number;
  fullName: string;
  teamName: string;
  fullNameWithTeam?: string;
}
export interface Car {
  id: number;
  brand: string;
  registrationPlate: string;
  employeeId: number;
  employeeName?: string;
}
export interface AddOfficePresence {
  date: Date;
  roomId: number;
  employeeId: string;
  parkingSpot?: number;
  carId?: number;
  parkingArrivalTime?: string;
  parkingDepartureTime?: string;
  notes?: string;
}
export interface GetOfficePresence extends AddOfficePresence {
  id: number;
  roomNumber: number;
  employeeName: string;
  employeeTeam: string;
}
export interface Team {
  id: number;
  shortName: string;
  fullName: string;
}
export interface ParkingSpot {
  id: number;
  spotNumber: number;
}
export interface Room {
  id: number;
  number: number;
}

export interface EmployeeDisplay {
  id: number;
  displayName: string;
}
