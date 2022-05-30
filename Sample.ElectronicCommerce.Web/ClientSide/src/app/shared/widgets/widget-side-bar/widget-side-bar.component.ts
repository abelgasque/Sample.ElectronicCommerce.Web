import { Component, OnInit } from '@angular/core';

import { SecurityService } from 'src/app/core/modules/security/security.service';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-widget-side-bar',
  templateUrl: './widget-side-bar.component.html',
  styleUrls: ['./widget-side-bar.component.css']
})
export class WidgetSideBarComponent implements OnInit {

  constructor(    
    public securityService: SecurityService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void { }
}