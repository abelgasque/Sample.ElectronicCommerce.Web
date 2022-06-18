import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from '../../../util/services/shared.service';
import { ReturnDTO } from 'src/app/util/entities/dto/return.dto';

@Component({
  selector: 'app-widget-sidebar-user',
  templateUrl: './widget-sidebar-user.component.html',
  styleUrls: ['./widget-sidebar-user.component.css']
})
export class WidgetSidebarUserComponent implements OnInit {

  public listUserSessions: any[] = [];

  constructor(
    public coreService: CoreService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void {
    this.getAll();
  }
  
  public getAll() {
    this.sharedService.openSpinner();
    // this.userSessionService.GetAll(null).subscribe({
    //   next: (response: ReturnDTO) => {
    //     if(response.isSuccess){
    //       this.listUserSessions = response.resultObject;
    //     }
    //     this.sharedService.closeSpinner();
    //   },
    //   error: (error) => {        
    //     this.coreService.errorHandler(error);        
    //   }
    // });
  }
}
