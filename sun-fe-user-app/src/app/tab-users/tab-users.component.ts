import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { ApiServiceService } from '../api-service.service';
import { IUser } from '../models/user';
import { WsServiceService } from '../ws-service.service';

export interface DialogData {
  confirmTitle: string;
  confirmMsg: string;
}

const ELEMENT_DATA: IUser[] = [
  {email: 'a1@aa.com'},
  {email: 'a2@aa.com'},
  {email: 'a3@aa.com'},
  {email: 'a4@aa.com'},
  {email: 'a5@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a8@aa.com'},
  {email: 'a1@aa.com'},
  {email: 'a2@aa.com'},
  {email: 'a3@aa.com'},
  {email: 'a4@aa.com'},
  {email: 'a5@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a8@aa.com'},
  {email: 'a1@aa.com'},
  {email: 'a2@aa.com'},
  {email: 'a3@aa.com'},
  {email: 'a4@aa.com'},
  {email: 'a5@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a8@aa.com'},
  {email: 'a1@aa.com'},
  {email: 'a2@aa.com'},
  {email: 'a3@aa.com'},
  {email: 'a4@aa.com'},
  {email: 'a5@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a6@aa.com'},
  {email: 'a8@aa.com'},
]

@Component({
  selector: 'app-tab-users',
  templateUrl: './tab-users.component.html',
  styleUrls: ['./tab-users.component.scss']
})
export class TabUsersComponent implements OnInit, AfterViewInit {
  public users: IUser[] = [];
  displayedColumns: string[] = ['email'];
  dataSource!: MatTableDataSource<IUser>;
  selection = new SelectionModel<IUser>(true, []);
  private readonly newUserTopic: string = 'new-user';

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sorter!: MatSort;
  
  constructor(public dialog: MatDialog, private _apiService: ApiServiceService,
    private _wsService: WsServiceService) {

   }


  ngAfterViewInit() {
    
  }

  ngOnInit(): void {
    this._apiService.fetchUsersFromServer().subscribe(
      data => {
        console.log('received users: ', data);
        this.users = data;
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sorter;
      }, 
      error => {
        console.log(error);
      });
    
      this._wsService.listen(this.newUserTopic).subscribe(data => {
        console.log('receive ws msg: ', data);
        const result = this.users.find( ({ email }) => email === data );
        if(result === undefined){
          this.users.push({email: data as string});
          location.reload(); 
        }
      });
  }

}
