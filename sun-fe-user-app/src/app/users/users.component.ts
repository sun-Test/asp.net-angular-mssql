import { getTreeMultipleDefaultNodeDefsError } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatListOption } from '@angular/material/list';
import { ActivatedRoute } from '@angular/router';
import { ApiServiceService } from '../api-service.service';
import { IUser } from '../models/user';
import { IVoting } from '../models/voting';
import { WsServiceService } from '../ws-service.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public accName: string = '';
  votings: IVoting[] = [];

  constructor(private _apiService: ApiServiceService, private route: ActivatedRoute, 
    private _wsService: WsServiceService) { }

  ngOnInit() {
    this._apiService.fetchVotingsFromServer().subscribe(data => this.votings = data);
    console.log( 'acc param: ', this.route.snapshot.params['acc']);
    this.accName = this.route.snapshot.params['acc'];
  }

  onCandidate(changedEvent: any){

    if(changedEvent.checked){    
      const res = this.votings.find(x=>x.candidateEmail === this.accName);
      if(res === undefined){
        this._apiService.createVoting({candidateEmail: this.accName}).subscribe (
          () => { this.votings.push({candidateEmail: this.accName});
          location.reload();  
        }
        );
      }
    }
    console.log('cadidate is checked: ', changedEvent.checked, this.accName);
  }

  onVote(selectedOptions: any){
    console.log("selected items: ", selectedOptions);
    for(let entry of selectedOptions){
      this._apiService.vote({candidateEmail: entry.value, voterEmail: this.accName}).subscribe(
        ()=>{
          location.reload();
        }
      );
      console.log(entry.value);
    }
  }

  isVotingInvalid(selectionList: any) {
    if(selectionList.selectedOptions.selected.length > 3){
      return true;
    }
    return false;
  }

}
