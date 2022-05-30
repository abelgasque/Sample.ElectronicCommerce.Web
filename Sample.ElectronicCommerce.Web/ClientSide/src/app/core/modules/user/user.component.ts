import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { UserService } from 'src/app/core/modules/user/user.service';

import { ReturnDTO, User } from 'src/app/shared/util/model';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  public listGrid: any[] = [];
  public dataForm: User = new User();
  public selectedTab = new FormControl(0);
  
  constructor(
    private coreService: CoreService,    
    private entityService: UserService,
    private sharedService: SharedService,    
  ) { }

  ngOnInit(): void {
    this.getAll();
  }

  public getAll() {
    this.sharedService.openSpinner();
    this.entityService.GetAll(null).subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          this.listGrid = response.resultObject;
          this.selectedTab.setValue(0);
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
          let entity: User = response.resultObject;  
          entity.dtCreation = new Date(entity.dtCreation);
          if(entity.dtLastUpdate != null) {
            entity.dtLastUpdate = new Date(entity.dtLastUpdate);
          }
          this.dataForm = entity;
          this.selectedTab.setValue(1);
          this.sharedService.closeSideBar();
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }  

  public newEntity(pEvent: boolean) {
    if(pEvent) {
      this.dataForm = new User();
      this.selectedTab.setValue(1);
    }
  }

  public reload(pEvent: boolean) {
    if(pEvent){
      this.getAll();      
    }
  }

  public persist(pEvent: boolean) {
    if(pEvent){
      this.getAll();      
    }
    this.selectedTab.setValue(0);
  }
}
