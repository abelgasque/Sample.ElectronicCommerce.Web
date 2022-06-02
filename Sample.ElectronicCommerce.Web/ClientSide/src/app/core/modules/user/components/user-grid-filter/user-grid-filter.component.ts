import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { UserEntity } from 'src/app/shared/util/Entities/UserEntity';

import { CoreService } from 'src/app/core/core.service';

@Component({
  selector: 'app-user-grid-filter',
  templateUrl: './user-grid-filter.component.html',
  styleUrls: ['./user-grid-filter.component.css']
})
export class UserGridFilterComponent implements OnInit {

  @Input() listGrid: any[];
  @Input() listPageOptions: number[] = [10,25,50,100];
  @Input() page = 0;
  @Input() rows = 100;  

  @Output() eventReload: EventEmitter<any> = new EventEmitter();
  @Output() eventNewEntity: EventEmitter<any> = new EventEmitter();
  @Output() eventGetById: EventEmitter<any> = new EventEmitter();

  public data: UserEntity = new UserEntity();

  constructor(
    public coreService: CoreService,
  ) { }

  ngOnInit(): void {  }

  public next() {
    if((this.page + this.rows) < this.listGrid.length){
      this.page = this.page + this.rows;
    }
  }

  public prev() {
    if((this.page - this.rows) > 0){
      this.page = this.page - this.rows;
    }
  }

  public isLastPage(): boolean {
    return this.listGrid ? this.page <= (this.listGrid.length - this.rows): true;
  }

  public isFirstPage(): boolean {
    return this.listGrid ? this.page === 0 : true;
  }

  public newEntity() {
    this.eventNewEntity.emit(true);
  }

  public getById(pId: number) {    
    this.eventGetById.emit(pId);
  }

  public reload(){
    this.eventReload.emit(true);
  }
}