import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { MailBrokerEntity } from 'src/app/shared/util/Entities/MailBrokerEntity';

import { CoreService } from 'src/app/core/core.service';
import { MailBrokerService } from '../../mail-broker.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-mail-broker-form-persist',
  templateUrl: './mail-broker-form-persist.component.html',
  styleUrls: ['./mail-broker-form-persist.component.css']
})
export class MailBrokerFormPersistComponent implements OnInit {

  @Input() data: MailBrokerEntity;
  @Output() eventPersist: EventEmitter<boolean> = new EventEmitter();

  public listActive: any[] = [
    { name: 'Ativo', value: true },
    { name: 'Inativo', value: false },
  ];

  public listYesOrNo: any[] = [
    { name: 'Sim', value: true },
    { name: 'Não', value: false },
  ];

  constructor(
    public coreService: CoreService,
    private entityService: MailBrokerService,
    private sharedService: SharedService,
  ) {  }

  ngOnInit(): void {
  }

  public cancelPersist(){
    this.eventPersist.emit(false);
  }

  public persistEntity(){
    if(this.data.id > 0){
      this.update();
    }else{
      this.insert();      
    }
  }

  public insert(){    
    this.sharedService.openSpinner();
    this.entityService.Insert(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: MailBrokerEntity = response.resultObject;          
          entity.dtCreation = new Date(entity.dtCreation);
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

  public update(){
    this.sharedService.openSpinner();
    this.entityService.Update(this.data).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          let entity: MailBrokerEntity = response.resultObject;          
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
}
