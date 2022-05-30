import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { Mail, ReturnDTO } from 'src/app/shared/util/model';
import { CoreService } from 'src/app/core/core.service';
import { MailService } from '../../mail.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-mail-form-persist',
  templateUrl: './mail-form-persist.component.html',
  styleUrls: ['./mail-form-persist.component.css']
})
export class MailFormPersistComponent implements OnInit {

  @Input() data: Mail;
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
    private entityService: MailService,
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
          let entity: Mail = response.resultObject;          
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
          let entity: Mail = response.resultObject;          
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
