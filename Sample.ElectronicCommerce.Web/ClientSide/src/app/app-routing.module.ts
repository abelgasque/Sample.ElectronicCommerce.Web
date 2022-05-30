import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CoreComponent } from './core/core.component';
import { DashboardLogAppComponent } from './core/modules/dashboard/components/dashboard-log-app/dashboard-log-app.component';
import { DashboardComponent } from './core/modules/dashboard/dashboard.component';

import { DefaultLayoutComponent } from './core/modules/default-layout/default-layout.component';
import { PageHomeComponent } from './core/modules/default-layout/layouts/page-home/page-home.component';
import { PageNotFoundComponent } from './core/modules/default-layout/layouts/page-not-found/page-not-found.component';

import { LogAppComponent } from './core/modules/log-app/log-app.component';

import { MailComponent } from './core/modules/mail/mail.component';
import { MailBrokerComponent } from './core/modules/mail-broker/mail-broker.component';
import { MailMessageComponent } from './core/modules/mail-message/mail-message.component';

import { SecurityComponent } from './core/modules/security/security.component';
import { LoginComponent } from './core/modules/security/layouts/login/login.component';

import { UserComponent } from './core/modules/user/user.component';
import { UserSessionComponent } from './core/modules/user-session/user-session.component';

import { ChatComponent } from './core/modules/chat/chat.component';

const routes: Routes = [ 
  //{ path: '', loadChildren: () => import('src/app/core/core.module').then(m => m.CoreModule) },   
  {
    path: '',
    component: CoreComponent,
    children: [
      { path: 'chat', component: ChatComponent },
      { 
        path: '', 
        component: DefaultLayoutComponent,
        children: [
          { path: '', component: PageHomeComponent },  
          { path: 'page-not-found', component: PageNotFoundComponent },  
        ]
      },
      { 
        path: 'dashboard', 
        component: DashboardComponent,
        children: [
          { path: 'log-app', component: DashboardLogAppComponent },
        ]
      },
      { path: 'log-app', component: LogAppComponent },
      { path: 'mail', component: MailComponent },      
      { path: 'mail-broker', component: MailBrokerComponent },
      { path: 'mail-message', component: MailMessageComponent },
      { 
        path: 'security', 
        component: SecurityComponent,
        children: [
          { path: 'auth', component: LoginComponent },
        ] 
      },
      { path: 'user', component: UserComponent },
      { path: 'user-session', component: UserSessionComponent },
    ]
  },
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path: '**', redirectTo: 'page-not-found' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }