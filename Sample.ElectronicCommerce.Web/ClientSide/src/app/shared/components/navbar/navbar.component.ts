import { Component, OnInit } from '@angular/core';

import { CoreService } from 'src/app/core/core.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    public coreService: CoreService,
    public securityService: SecurityService,
    public sharedService: SharedService,
  ) { }

  ngOnInit() { }
}
