import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { UserSession } from 'src/app/shared/util/model';

@Component({
  selector: 'app-user-session-model-persist',
  templateUrl: './user-session-model-persist.component.html',
  styleUrls: ['./user-session-model-persist.component.css']
})
export class UserSessionModelPersistComponent implements OnInit {

  @Input() data: UserSession;
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
