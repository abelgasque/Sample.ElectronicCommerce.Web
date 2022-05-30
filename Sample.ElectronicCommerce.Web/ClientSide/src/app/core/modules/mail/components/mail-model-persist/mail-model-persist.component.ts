import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Mail } from 'src/app/shared/util/model';

@Component({
  selector: 'app-mail-model-persist',
  templateUrl: './mail-model-persist.component.html',
  styleUrls: ['./mail-model-persist.component.css']
})
export class MailModelPersistComponent implements OnInit {

  @Input() data: Mail;
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
