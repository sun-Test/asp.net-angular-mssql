import { Component, OnInit } from '@angular/core';
import { WsServiceService } from './ws-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'sun-ws-angu-client';

  //inject dependence
  constructor() { 
  }

  ngOnInit() {
  }
}
