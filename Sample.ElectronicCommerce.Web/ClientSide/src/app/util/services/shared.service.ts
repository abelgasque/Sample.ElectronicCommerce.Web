import { Injectable } from '@angular/core';

import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  public listMenu: any[] = [];
  public openedSpinner: boolean = false;
  public openedSidebarMenu: boolean = false;
  public openedSidebarChat: boolean = false;
  public openedSidebarUser: boolean = false;
  public openedSidebarUserLead: boolean = false;

  constructor(private messageService: MessageService) { }

  public getListMenu() {
    this.listMenu = [
      {
        name: "Documentação",
        icon: "file",
        isOpen: false,
        items: [
          { name: "Principal", icon: "home", routerLink: "" },
        ]
      },
    ];
  }

  public closeAllSidebar() {
    this.openedSidebarMenu = false;
    this.openedSidebarChat = false;
    this.openedSidebarUser = false;
  }

  public toggleSidebarMenu() {
    this.openedSidebarChat = false;
    this.openedSidebarUser = false;
    this.openedSidebarUserLead = false;
    this.openedSidebarMenu = !this.openedSidebarMenu;
  }

  public toggleSidebarChat() {
    this.openedSidebarMenu = false;
    this.openedSidebarUser = false;
    this.openedSidebarUserLead = false;
    this.openedSidebarChat = !this.openedSidebarChat;
  }

  public toggleSidebarUser() {
    this.openedSidebarMenu = false;
    this.openedSidebarChat = false;
    this.openedSidebarUserLead = false;
    this.openedSidebarUser = !this.openedSidebarUser;
  }

  public toggleSidebarUserLead() {
    this.openedSidebarMenu = false;
    this.openedSidebarChat = false;
    this.openedSidebarUser = false;
    this.openedSidebarUserLead = !this.openedSidebarUserLead;
  }

  public openSpinner() {
    this.openedSpinner = true;
  }

  public closeSpinner() {
    this.openedSpinner = false;
  }

  public showMessageSuccess(pDeDetail: string) {
    this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: pDeDetail });
  }

  public showMessageInfo(pDeDetail: string) {
    this.messageService.add({ severity: 'info', summary: 'Informações', detail: pDeDetail });
  }

  public showMessageWarn(pDeDetail: string) {
    this.messageService.add({ severity: 'warn', summary: 'Aviso', detail: pDeDetail });
  }

  public showMessageError(pDeDetail: string) {
    this.messageService.add({ severity: 'error', summary: 'Erro', detail: pDeDetail });
  }
}
