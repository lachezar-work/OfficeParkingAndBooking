export interface Employee {
  id: number;
  name: string;
  team: Team;
}

export interface Car {
  id: number;
  employeeId: number;
  brand: string;
  registrationPlate: string;
}

export interface OfficePresence {
  id: number;
  date: Date;
  employeeId: number;
  roomId: number;
  parkingSpot?: number;
  parkingArrivalTime?: string;
  parkingDepartureTime?: string;
  notes?: string;
}

export interface Team {
  id: number;
  shortName: string;
  fullName: string;
}

export interface Room {
  id: number;
  number: number;
}
