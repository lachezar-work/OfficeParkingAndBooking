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

export enum Team {
  BA = 'BA',
  HR = 'HR',
  SysAdmin = 'Sys Admin',
  DevOps = 'DevOps',
  Java = 'Java',
  DotNet = '.NET',
  AM = 'AM',
  FO = 'FO'
}

export enum Room {
  Room403 = 'Room 403',
  Room404 = 'Room 404'
}
