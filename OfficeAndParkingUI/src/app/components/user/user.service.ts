import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable } from 'rxjs';

import { IUser, IUserCredentials } from './user.model';
import { RegisterModel } from '../../models/register.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user: BehaviorSubject<IUser | null>;

  constructor(private http: HttpClient) {
    this.user = new BehaviorSubject<IUser | null>(null);
  }

  getUser(): Observable<IUser | null> {
    return this.user;
  }

  signIn(credentials: IUserCredentials): Observable<IUser> {
    return this.http
      .post<IUser>('/api/employee/login', credentials)
      .pipe(map((user: IUser) => {
        this.user.next(user);
        return user;
      }));
  }

  signOut() {
    this.user.next(null);
    return this.http.post('/api/employee/logout', {}, {
      withCredentials: true,
      observe: 'response',
      responseType: 'text'
    });
  }

  register(registerData: RegisterModel): Observable<any> {
    return this.http.post('/api/employee/register', registerData);
  }

  checkLoginStatus(): Observable<IUser | null> {
    return this.http.get<IUser>('/api/employee/current').pipe(
      map((user: IUser) => {
        this.user.next(user);
        return user;
      })
    );
  }

  isLoggedIn(): boolean {
    return this.user.value !== null;
  }
}
