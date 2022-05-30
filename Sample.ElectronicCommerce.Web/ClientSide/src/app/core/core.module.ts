import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatSidenavModule } from '@angular/material/sidenav';

import { CoreRoutingModule } from './core-routing.module';

import { ChatModule } from './modules/chat/chat.module';
import { DashboardModule } from 'src/app/core/modules/dashboard/dashboard.module';
import { LogAppModule } from 'src/app/core/modules/log-app/log-app.module';
import { MailModule } from 'src/app/core/modules/mail/mail.module';
import { MailBrokerModule } from 'src/app/core/modules/mail-broker/mail-broker.module';
import { MailMessageModule } from 'src/app/core/modules/mail-message/mail-message.module';
import { UserModule } from 'src/app/core/modules/user/user.module';
import { UserSessionModule } from 'src/app/core/modules/user-session/user-session.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DefaultLayoutModule } from './modules/default-layout/default-layout.module';
import { SecurityModule } from './modules/security/security.module';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { LogAppService } from './modules/log-app/log-app.service';
import { MailService } from './modules/mail/mail.service';
import { MailBrokerService } from './modules/mail-broker/mail-broker.service';
import { MailMessageService } from './modules/mail-message/mail-message.service';
import { SecurityService } from './modules/security/security.service';
import { UserService } from './modules/user/user.service';
import { UserSessionService } from './modules/user-session/user-session.service';

import { CoreComponent } from 'src/app/core/core.component';
import { ChatService } from './modules/chat/chat.service';

@NgModule({
  declarations: [
    CoreComponent
  ],
  imports: [
    CoreRoutingModule,    
    CommonModule,
    RouterModule,
     
    MatSidenavModule,    
         
    ChatModule,
    DashboardModule,
    DefaultLayoutModule,
    LogAppModule,
    MailModule,
    MailBrokerModule,
    MailMessageModule,
    UserModule,
    UserSessionModule,
    SecurityModule,
    SharedModule,
  ],
  providers:[ 
    CoreService,
    ChatService,
    LogAppService,
    MailService,
    MailBrokerService,
    MailMessageService,
    SharedService, 
    SecurityService,
    UserService,
    UserSessionService,    
  ]
})
export class CoreModule { }
