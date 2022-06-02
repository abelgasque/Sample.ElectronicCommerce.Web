import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MailEntity } from 'src/app/shared/util/Entities/MailEntity';

@Component({
  selector: 'app-mail-model-persist',
  templateUrl: './mail-model-persist.component.html',
  styleUrls: ['./mail-model-persist.component.css']
})
export class MailModelPersistComponent implements OnInit {

  @Input() data: MailEntity;
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
