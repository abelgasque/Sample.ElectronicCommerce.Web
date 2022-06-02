import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/core.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';
import { UserSessionService } from 'src/app/core/modules/user-session/user-session.service';
import { SharedService } from '../../shared.service';
import { ReturnDTO } from '../../util/EntitiesDTO/ReturnDTO';

@Component({
  selector: 'app-widget-sidebar-user',
  templateUrl: './widget-sidebar-user.component.html',
  styleUrls: ['./widget-sidebar-user.component.css']
})
export class WidgetSidebarUserComponent implements OnInit {

  public listUserSessions: any[] = [];

  constructor(
    public coreService: CoreService,
    public securityService: SecurityService,
    public sharedService: SharedService,
    public userSessionService: UserSessionService,
  ) { }

  ngOnInit(): void {
    this.getAll();
  }
  
  public getAll() {
    this.sharedService.openSpinner();
    this.userSessionService.GetAll(null).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          this.listUserSessions = response.resultObject;
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {        
        this.coreService.errorHandler(error);        
      }
    });
  }
}
