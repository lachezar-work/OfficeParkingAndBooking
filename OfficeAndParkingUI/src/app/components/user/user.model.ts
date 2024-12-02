export interface IUser {
  firstName: string;
  lastName: string;
  username: string;
  employeeName?: string;
  employeeTeam?: string;
  password?: string;
}

export interface IUserCredentials {
  username: string;
  password: string;
}
