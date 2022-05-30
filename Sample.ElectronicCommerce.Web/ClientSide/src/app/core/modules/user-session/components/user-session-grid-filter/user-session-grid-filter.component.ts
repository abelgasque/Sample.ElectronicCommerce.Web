import { Component, OnInit } from '@angular/core';

import { ReturnDTO, UserSession } from 'src/app/shared/util/model';
import { CoreService } from 'src/app/core/core.service';
import { UserSessionService } from '../../user-session.service';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-user-session-grid-filter',
  templateUrl: './user-session-grid-filter.component.html',
  styleUrls: ['./user-session-grid-filter.component.css']
})
export class UserSessionGridFilterComponent implements OnInit {

  public listGrid: any[] = [];
  public first = 0;
  public rows = 100;
  public isOpenDialog: boolean = false;
  public data: UserSession = new UserSession();

  constructor(
    private coreService: CoreService,
    private entityService: UserSessionService, 
    private sharedService: SharedService,
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  public eventPersistUserModel(pEvent: boolean) {
    if(pEvent == true){
      this.getAll();      
    }
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

  public formatDatePtBr(date: string){
    return this.coreService.formatDatePtBr(date);
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
          if(this.data.dtLastUpdate != null){ 
            this.data.dtLastUpdate = new Date(this.data.dtLastUpdate);
          }
          this.sharedService.closeSideBar();
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
