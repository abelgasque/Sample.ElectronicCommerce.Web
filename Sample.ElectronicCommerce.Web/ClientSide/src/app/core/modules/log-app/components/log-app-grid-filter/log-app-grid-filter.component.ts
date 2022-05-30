import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { LogApp, ReturnDTO } from 'src/app/shared/util/model';
import { LogAppService } from '../../log-app.service';

@Component({
  selector: 'app-log-app-grid-filter',
  templateUrl: './log-app-grid-filter.component.html',
  styleUrls: ['./log-app-grid-filter.component.css']
})
export class LogAppGridFilterComponent implements OnInit {

  public listGrid: any[] = [];
  public first = 0;
  public rows = 100;
  public isOpenDialog: boolean = false;
  public data: LogApp = new LogApp();

  constructor(
    private coreService: CoreService,
    private logAppService: LogAppService, 
    private sharedService: SharedService
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
    this.logAppService.GetAll(null).subscribe({
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
    this.logAppService.GetById(pId).subscribe({
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
