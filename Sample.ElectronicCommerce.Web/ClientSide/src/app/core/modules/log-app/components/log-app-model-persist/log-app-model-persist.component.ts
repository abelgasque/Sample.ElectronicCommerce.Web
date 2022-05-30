import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LogApp } from 'src/app/shared/util/model';

@Component({
  selector: 'app-log-app-model-persist',
  templateUrl: './log-app-model-persist.component.html',
  styleUrls: ['./log-app-model-persist.component.css']
})
export class LogAppModelPersistComponent implements OnInit {

  @Input() data: LogApp;
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
