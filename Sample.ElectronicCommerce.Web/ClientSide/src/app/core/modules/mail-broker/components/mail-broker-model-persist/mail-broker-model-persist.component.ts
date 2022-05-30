import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MailBroker } from 'src/app/shared/util/model';

@Component({
  selector: 'app-mail-broker-model-persist',
  templateUrl: './mail-broker-model-persist.component.html',
  styleUrls: ['./mail-broker-model-persist.component.css']
})
export class MailBrokerModelPersistComponent implements OnInit {

  @Input() data: MailBroker;
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
