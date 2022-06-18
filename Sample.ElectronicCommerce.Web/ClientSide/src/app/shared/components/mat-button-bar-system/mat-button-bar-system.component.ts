import { Component, OnInit } from '@angular/core';

import { SharedService } from '../../../util/services/shared.service';

@Component({
  selector: 'app-mat-button-bar-system',
  templateUrl: './mat-button-bar-system.component.html',
  styleUrls: ['./mat-button-bar-system.component.css']
})
export class MatButtonBarSystemComponent implements OnInit {

  constructor(    
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void {
  }

}
