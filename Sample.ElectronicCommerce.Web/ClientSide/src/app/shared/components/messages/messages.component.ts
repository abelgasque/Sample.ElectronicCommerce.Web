import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../util/services/shared.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  constructor(public sharedService: SharedService) { }

  ngOnInit(): void {
  }

}
