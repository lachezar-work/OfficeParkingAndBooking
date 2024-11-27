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
export interface AddOfficePresence {
  date: Date;
  roomNumber: number;
  parkingSpot?: number;
  parkingArrivalTime?: string;
  parkingDepartureTime?: string;
  notes?: string;
}
export interface GetOfficePresence extends AddOfficePresence {
  id: number;
  employeeName: string;
  employeeTeam: string;
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
