import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import * as signalR from "@microsoft/signalr";

import { environment } from 'src/environments/environment';

import { SharedService } from 'src/app/shared/shared.service';
import { ReturnDTO } from 'src/app/shared/util/model';

import { CoreService } from '../../core.service';
import { SecurityService } from '../security/security.service';
import { ChatService } from './chat.service';

interface Message {
  userName: string;
  text: string;
}

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public messages: Message[] = [];
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
    this.connection = new signalR.HubConnectionBuilder().withUrl(`${ environment.baseUrl }/chat`).build();
    this.startConnection();
    this.getAll();
  }

  ngOnInit(): void {
  }

  public getNameUserAuth() : string {
    return this.securityService.userSession.user.name + " " + this.securityService.userSession.user.lastName;
  }

  private startConnection(){
    this.connection.on("ReceiveMessage", (userName: string, message: string) => {
      this.messages.push({ userName: userName, text: message } );
    });

    this.connection.start();
  }

  private setForm() {
    this.form = this.formBuilder.group({
      message: [null],
    });
  }

  public sendMessage() {
    if(this.message.length > 0) {
      this.connection.send("SendMessage", this.getNameUserAuth(), this.message)
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