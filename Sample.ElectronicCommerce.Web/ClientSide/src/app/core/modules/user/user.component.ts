import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { UserService } from 'src/app/core/modules/user/user.service';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { UserEntity } from 'src/app/shared/util/Entities/UserEntity';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  public listGrid: any[] = [];
  public dataForm: UserEntity = new UserEntity();
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
          let entity: UserEntity = response.resultObject;  
          entity.dtCreation = new Date(entity.dtCreation);
          if(entity.dtLastUpdate != null) {
            entity.dtLastUpdate = new Date(entity.dtLastUpdate);
          }
          this.dataForm = entity;
          this.selectedTab.setValue(1);
          this.sharedService.closeAllSidebar();
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
      this.dataForm = new UserEntity();
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
