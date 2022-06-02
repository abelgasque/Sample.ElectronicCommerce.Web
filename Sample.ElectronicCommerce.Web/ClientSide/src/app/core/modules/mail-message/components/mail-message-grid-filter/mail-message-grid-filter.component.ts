import { Component, OnInit } from '@angular/core';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { UserEntity } from 'src/app/shared/util/Entities/UserEntity';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { MailMessageService } from 'src/app/core/modules/mail-message/mail-message.service';

@Component({
  selector: 'app-mail-message-grid-filter',
  templateUrl: './mail-message-grid-filter.component.html',
  styleUrls: ['./mail-message-grid-filter.component.css']
})
export class MailMessageGridFilterComponent implements OnInit {

  public listGrid: any[] = [];
  public first = 0;
  public rows = 100;
  public isOpenDialog: boolean = false;
  public data: UserEntity = new UserEntity();

  constructor(
    private coreService: CoreService,    
    private entityService: MailMessageService,
    private sharedService: SharedService, 
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  public formatDatePtBr(pDate: string) {
    return this.coreService.formatDatePtBr(pDate);
  }

  public eventPersistUserModel(pEvent: boolean) {
    if(pEvent == true){
      this.getAll();      
    }
  }

  public newEntity() {
    this.data = new UserEntity();
    this.sharedService.closeAllSidebar();
    this.isOpenDialog = true;
  }

  public next() {
    if((this.first + this.rows) < this.listGrid.length){
      this.first = this.first + this.rows;
    }
  }

  public prev() {
    if((this.first - this.rows) > 0){
      this.first = this.first - this.rows;
    }
  }

  public isLastPage(): boolean {
    return this.listGrid ? this.first <= (this.listGrid.length - this.rows): true;
  }

  public isFirstPage(): boolean {
    return this.listGrid ? this.first === 0 : true;
  }

  public getAll() {
    this.sharedService.openSpinner();
    this.entityService.GetAll(null).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          this.listGrid = response.resultObject;
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }

  public getById(pId: number) {    
    this.sharedService.openSpinner();
    this.entityService.GetById(pId).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){          
          this.data = response.resultObject;  
          this.data.dtCreation = new Date(this.data.dtCreation);
          if(this.data.dtLastUpdate != null) {
            this.data.dtLastUpdate = new Date(this.data.dtLastUpdate);
          }
          this.sharedService.closeAllSidebar();
          this.isOpenDialog = true;
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }
}