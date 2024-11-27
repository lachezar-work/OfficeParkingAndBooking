export interface IUser {
  firstName: string;
  lastName: string;
  username: string;
  password?: string;
}

export interface IUserCredentials {
  username: string;
  password: string;
}
