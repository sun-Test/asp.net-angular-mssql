import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MatTableDataSource} from '@angular/material/table';
import { IUser } from '../models/user';

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
]

@Component({
  selector: 'app-tab-users',
  templateUrl: './tab-users.component.html',
  styleUrls: ['./tab-users.component.scss']
})
export class TabUsersComponent implements OnInit {
  displayedColumns: string[] = ['select', 'email'];
  dataSource = new MatTableDataSource<IUser>(ELEMENT_DATA);
  selection = new SelectionModel<IUser>(true, []);
  
  constructor(public dialog: MatDialog) { }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    console.log(this.selection.selected.length);
  }

  checkboxLabel(row: any) {
    if(this.selection.selected.length > 2){
      this.selection.deselect(row);
      this.openDialog();
    }else{
      this.selection.toggle(row);
    }
    console.log(this.selection.selected.length);
  }

  openDialog() {
    this.dialog.open(ConfirmDialog, {
      data: {
        confirmTitle: 'your votings have exceeded the limitation',
        confirmMsg: 'the max voting is 3'
      }
    });
  }

  ngOnInit(): void {
  }

}


@Component({
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.html',
})
export class ConfirmDialog {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) {}
}