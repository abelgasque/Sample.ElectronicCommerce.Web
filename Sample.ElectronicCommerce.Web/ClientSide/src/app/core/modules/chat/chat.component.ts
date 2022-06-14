import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import * as signalR from "@microsoft/signalr";

import { environment } from 'src/environments/environment';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';
import { ChatMessageEntity } from 'src/app/shared/util/Entities/ChatMessageEntity';

import { SharedService } from 'src/app/shared/shared.service';
import { CoreService } from 'src/app/core/core.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';
import { ChatService } from 'src/app/core/modules/chat/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public messages: ChatMessageEntity[] = [];
  public message: string = null;
  public form: FormGroup;
  public connection;
  
  constructor(
    private formBuilder: FormBuilder,
    private coreService: CoreService,
    private securityService: SecurityService,
    private sharedService: SharedService,
    private entityService: ChatService,
  ) {
    this.setForm();
    this.connection = new signalR.HubConnectionBuilder().withUrl(`${ environment.baseUrl }/chat/broker`).build();
    this.startConnection();
    this.getAll();
  }

  ngOnInit(): void {
  }

  public getNameUserAuth() : string {
    return this.securityService.userSession.user.name + " " + this.securityService.userSession.user.lastName;
  }

  private startConnection(){
    this.connection.on("ReceiveMessageChatBrokerAll", (data: ChatMessageEntity, message: string) => {
      this.messages.push(data);
    });

    this.connection.start();
  }

  private setForm() {
    this.form = this.formBuilder.group({
      message: [null],
    });
  }

  public sendMessageChatBrokerAll() {
    if(this.message.length > 0) {
      let newMessage = new ChatMessageEntity();
      newMessage.name = this.getNameUserAuth();
      newMessage.idUserSender = this.securityService.userSession.IdUser;
      newMessage.message = this.message;
      this.connection.send("SendMessageChatBrokerAll", newMessage)
      .then(() => {
        this.form.reset();
        this.message = null;
      });
    }
  }

  public getAll() {
    this.sharedService.openSpinner();
    this.entityService.GetAll().subscribe({
      next: (response: ReturnDTO) => {
        if(response.isSuccess){
          this.messages = response.resultObject;
        }
        this.sharedService.closeSpinner();
      },
      error: (error) => {
        this.coreService.errorHandler(error);
      }
    });
  }
}
