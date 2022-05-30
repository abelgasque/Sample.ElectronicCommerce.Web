import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from 'src/app/core/modules/security/auth.guard';

import { CoreComponent } from 'src/app/core/core.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    canActivate: [AuthGuard],
    children: [                     
      { path: '', loadChildren: () => import('src/app/core/modules/default-layout/default-layout.module').then(m => m.DefaultLayoutModule) },  
      { path: 'chat', loadChildren: () => import('src/app/core/modules/chat/chat.module').then(m => m.ChatModule) },
      { path: 'security', loadChildren: () => import('src/app/core/modules/security/security.module').then(m => m.SecurityModule) },      
      { path: 'dashboard', loadChildren: () => import('src/app/core/modules/dashboard/dashboard.module').then(m => m.DashboardModule) },      
      { path: 'user', loadChildren: () => import('src/app/core/modules/user/user.module').then(m => m.UserModule) },
      { path: 'user-session', loadChildren: () => import('src/app/core/modules/user-session/user-session.module').then(m => m.UserSessionModule) },
      { path: 'log-app', loadChildren: () => import('src/app/core/modules/log-app/log-app.module').then(m => m.LogAppModule) },
      { path: 'mail',  loadChildren: () => import('src/app/core/modules/mail/mail.module').then(m => m.MailModule) },
      { path: 'mail-broker', loadChildren: () => import('src/app/core/modules/mail-broker/mail-broker.module').then(m => m.MailBrokerModule) },
      { path: 'mail-message', loadChildren: () => import('src/app/core/modules/mail-message/mail-message.module').then(m => m.MailMessageModule) },
    ]
  }, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
