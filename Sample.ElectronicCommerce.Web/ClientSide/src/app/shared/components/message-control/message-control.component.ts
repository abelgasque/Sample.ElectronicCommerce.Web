import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-message-control',
  templateUrl: './message-control.component.html',
  styleUrls: ['./message-control.component.css']
})
export class MessageControlComponent implements OnInit {

  @Input() type: string = "info";
  @Input() message: string = "Insira uma mensagem para este componente!";

  constructor() { }

  ngOnInit(): void { }
}
