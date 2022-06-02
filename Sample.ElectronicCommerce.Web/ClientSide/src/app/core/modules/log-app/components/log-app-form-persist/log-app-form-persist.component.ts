import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { LogAppEntity } from 'src/app/shared/util/Entities/LogAppEntity';

import { CoreService } from 'src/app/core/core.service';
import { LogAppService } from '../../log-app.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-log-app-form-persist',
  templateUrl: './log-app-form-persist.component.html',
  styleUrls: ['./log-app-form-persist.component.css']
})
export class LogAppFormPersistComponent implements OnInit {

  @Input() data: LogAppEntity;
  @Output() eventPersist: EventEmitter<boolean> = new EventEmitter();

  public listActive: any[] = [
    { name: 'Ativo', value: true },
    { name: 'Inativo', value: false },
  ];

  constructor(
    public coreService: CoreService,
    private entityService: LogAppService,
    private sharedService: SharedService,
  ) 
  {  }

  ngOnInit(): void {
    //this.getAllRole();
  }

  public cancelPersist(){
    this.eventPersist.emit(false);
  }

  public persistEntity(){
    if(this.data.id > 0){
      this.update();
    }
  }

  public update(){
    this.sharedService.openSpinner();
    this.entityService.Update(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: LogAppEntity = response.resultObject;          
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
