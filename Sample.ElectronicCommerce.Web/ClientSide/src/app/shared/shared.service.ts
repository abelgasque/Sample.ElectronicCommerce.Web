import { Injectable } from '@angular/core';

import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class SharedService 
{  
  public listMenu: any[] = [];
  public openedSideBar: boolean = false;
  public openedSpinner: boolean = false;

  constructor(private messageService: MessageService) {}

  public getListMenu(){
    this.listMenu = [
      {          
        name: "Painel de Controle",         
        icon: "dashboard", 
        isOpen: false,
        items: [
          { name: "Histórico Aplicação", icon:"", routerLink: "dashboard/log-app" }
        ]
      },
      {          
        name: "Módulos",         
        icon: "view_list", 
        isOpen: false,
        items: [
          //{ name: "Agente E-mail", icon:"contact_mail", routerLink: "mail-broker" },
          //{ name: "E-mail", icon:"mail_outline", routerLink: "mail" },
          //{ name: "Mensagem E-mail", icon:"forward_to_inbox", routerLink: "mail-message" },          
          { name: "Chat", icon:"chat", routerLink: "chat" },
          { name: "Histórico Aplicação", icon:"travel_explore", routerLink: "log-app" },
          { name: "Sessão Usuário", icon:"admin_panel_settings", routerLink: "user-session" },
          { name: "Usuário", icon:"account_circle", routerLink: "user" },
        ]
      },
    ];
  }

  public toggleSideBar(){
    this.openedSideBar = !this.openedSideBar;
  }

  public openSideBar(){
    this.openedSideBar = true;
  }

  public closeSideBar(){
    this.openedSideBar = false;
  }

  public openSpinner(){
    this.openedSpinner = true;
  }

  public closeSpinner(){
    this.openedSpinner = false;
  }

  public showMessageSuccess(pDeDetail: string) {
    this.messageService.add({severity:'success', summary: 'Sucesso', detail: pDeDetail});
  }

  public showMessageInfo(pDeDetail: string) {
    this.messageService.add({severity:'info', summary: 'Informações', detail: pDeDetail});
  }

  public showMessageWarn(pDeDetail: string) {
    this.messageService.add({severity:'warn', summary: 'Aviso', detail: pDeDetail});
  }

  public showMessageError(pDeDetail: string) {
    this.messageService.add({severity:'error', summary: 'Erro', detail: pDeDetail});
  }
}
