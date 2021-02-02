import { getTreeMultipleDefaultNodeDefsError } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatListOption } from '@angular/material/list';
import { ActivatedRoute } from '@angular/router';
import { ApiServiceService } from '../api-service.service';
import { IUser } from '../models/user';
import { WsServiceService } from '../ws-service.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public accName: string = '';
  public users: IUser[] = [];
  private readonly newUserTopic: string = 'new-user';

  constructor(private _apiService: ApiServiceService, private route: ActivatedRoute, 
    private _wsService: WsServiceService) { }

  ngOnInit() {
    this._apiService.fetchUsersFromServer().subscribe(data => this.users = data);
    console.log( 'acc param: ', this.route.snapshot.params['acc']);
    this.accName = this.route.snapshot.params['acc'];

    this._wsService.listen(this.newUserTopic).subscribe(data => {
      console.log('receive ws msg: ', data);
      const result = this.users.find( ({ email }) => email === data );
      if(result === undefined){
        this.users.push({email: data as string});
        location.reload(); 
      }
    });
  }

  onCandidate(changedEvent: any){
    
    console.log('cadidate is checked: ', changedEvent.checked);
  }
  onVote(selectedOptions: any){
    console.log("selected items: ", selectedOptions.length);
  }

  isVotingInvalid(selectionList: any) {
    if(selectionList.selectedOptions.selected.length > 3){
      return true;
    }
    return false;
  }

}
