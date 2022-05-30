import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { MailMessage } from 'src/app/shared/util/model';


@Component({
  selector: 'app-mail-message-model-persist',
  templateUrl: './mail-message-model-persist.component.html',
  styleUrls: ['./mail-message-model-persist.component.css']
})
export class MailMessageModelPersistComponent implements OnInit {

  @Input() data: MailMessage;
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

  public eventPersistForm(pEvent: boolean){
    if(pEvent == false){
      this.visableDialog(false);      
    }
    this.eventPersist.emit(pEvent);
  }

}
