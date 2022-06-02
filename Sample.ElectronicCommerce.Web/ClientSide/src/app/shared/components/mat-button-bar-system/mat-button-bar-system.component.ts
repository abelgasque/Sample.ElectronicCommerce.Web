import { Component, OnInit } from '@angular/core';

import { SharedService } from '../../shared.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';

@Component({
  selector: 'app-mat-button-bar-system',
  templateUrl: './mat-button-bar-system.component.html',
  styleUrls: ['./mat-button-bar-system.component.css']
})
export class MatButtonBarSystemComponent implements OnInit {

  constructor(    
    public securityService: SecurityService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void {
  }

}
