import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from './models/user';
import { environment} from './../environments/environment'
import { IVoting } from './models/voting';

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

  createVoting(voting: IVoting): Observable<IVoting> {
    console.log("create voting " + environment.apiUrl + '/Voting/CreateVoting');
    return this.http.post<IVoting>( environment.apiUrl + '/Voting/CreateVoting', voting);
  }

  vote(voting: IVoting): Observable<IVoting> {
    console.log("create voting " + environment.apiUrl + '/Voting/vote');
    return this.http.post<IVoting>( environment.apiUrl + '/Voting/vote', voting);
  }

  fetchVotingsFromServer(): Observable<IVoting[]>{
    return this.http.get<IVoting[]>( environment.apiUrl + '/Voting/Votings');
  }
}
