import { Component, OnInit } from '@angular/core';

import { SecurityService } from 'src/app/core/modules/security/security.service';
import { SharedService } from './shared/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit 
{    
  public title = 'app';

  constructor(
    private sharedService: SharedService,
    private securityService: SecurityService,
  ) { }

  ngOnInit(): void {
    this.loadApplication();
  }

  public loadApplication() {
    this.sharedService.getListMenu();
    this.securityService.refreshUserSession();
  }
}
