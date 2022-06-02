import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { UserSessionEntity } from 'src/app/shared/util/Entities/UserSessionEntity';

@Component({
  selector: 'app-user-session-model-persist',
  templateUrl: './user-session-model-persist.component.html',
  styleUrls: ['./user-session-model-persist.component.css']
})
export class UserSessionModelPersistComponent implements OnInit {

  @Input() data: UserSessionEntity;
  @Input() isOpen: boolean = false;  
  @Output() eventOpen: EventEmitter<any> = new EventEmitter();
  @Output() eventPersist: EventEmitter<any> = new EventEmitter();
  
  constructor() { }

  ngOnInit(): void {
  }

  public visableDialog(pIsOpen: boolean) {
    this.isOpen = pIsOpen;
    this.eventOpen.emit(pIsOpen);
  }

  public eventPersistUserForm(pEvent: boolean){
    if(pEvent == false){
      this.visableDialog(false);      
    }
    this.eventPersist.emit(pEvent);
  }

}
