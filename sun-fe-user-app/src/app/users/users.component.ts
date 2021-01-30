import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiServiceService } from '../api-service.service';
import { IUser } from '../models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public accName: string = '';
  public users: IUser[] = [];
  constructor(private _apiService: ApiServiceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this._apiService.fetchUsersFromServer().subscribe(data => this.users = data);
    console.log( 'acc param: ', this.route.snapshot.params['acc']);
    this.accName = this.route.snapshot.params['acc'];
  }

}
