import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from './models/user';
import { environment} from './../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  constructor(private http: HttpClient) { }


  fetchUsersFromServer(): Observable<IUser[]>{
    return this.http.get<IUser[]>( environment.apiUrl + '/User/Users');
  }

  newUser(user: IUser): Observable<IUser> {
    console.log("post new user " + environment.apiUrl + '/User/NewUser');
    return this.http.post<IUser>( environment.apiUrl + '/User/NewUser', user);
  }

  login(user: IUser): Observable<IUser> {
    console.log("post new user " + environment.apiUrl + '/User/Login');
    return this.http.post<IUser>( environment.apiUrl + '/User/Login', user);
  }
}
