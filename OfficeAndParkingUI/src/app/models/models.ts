export interface Employee {
  id: number;
  fullName: string;
  teamName: string;
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
  employeeId: string;
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
