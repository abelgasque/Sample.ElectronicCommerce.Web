import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { ReturnDTO, UserSession } from 'src/app/shared/util/model';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { UserSessionService } from '../../user-session.service';

@Component({
  selector: 'app-user-session-form-persist',
  templateUrl: './user-session-form-persist.component.html',
  styleUrls: ['./user-session-form-persist.component.css']
})
export class UserSessionFormPersistComponent implements OnInit {

  @Input() data: UserSession;
  @Output() eventPersist: EventEmitter<boolean> = new EventEmitter();
  public listActive: any[] = [
    { name: 'Ativo', value: true },
    { name: 'Inativo', value: false },
  ];

  constructor(
    public coreService: CoreService,
    private entityService: UserSessionService,
    private sharedService: SharedService,
  ) {  }

  ngOnInit(): void { }

  public cancelPersist(){
    this.eventPersist.emit(false);
  }

  public persistEntity(){
    if(this.data.id != null){
      this.update();
    }
  }

  public update(){
    this.sharedService.openSpinner();
    this.entityService.Update(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: UserSession = response.resultObject;          
          entity.dtCreation = new Date(entity.dtCreation);
          entity.dtLastUpdate = (entity.dtLastUpdate != null) ? new Date(entity.dtLastUpdate) : null;
          this.data = entity;
          this.eventPersist.emit(true);
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }

  // public getAllRole() {
  //   this.coreService.openSpinner();
  //   this.userService.GetAllRole(true).subscribe({
  //     next: (response: ReturnDTO) => {
  //       if(response.isSuccess){
  //         this.listRole = response.resultObject;
  //       }
  //       this.coreService.closeSpinner();
  //     },
  //     error: (error) => {
  //       this.coreService.errorHandler(error);
  //       this.coreService.closeSpinner();
  //     }
  //   });
  // }
}
