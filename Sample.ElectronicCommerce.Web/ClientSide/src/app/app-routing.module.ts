import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CoreComponent } from './core/core.component';
import { DocumentationComponent } from './core/modules/documentation/documentation.component';
import { WelcomeComponent } from './core/modules/documentation/layouts/welcome/welcome.component';
import { AuthComponent } from './core/modules/security/layouts/auth/auth.component';
import { ForgotPasswordComponent } from './core/modules/security/layouts/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './core/modules/security/layouts/reset-password/reset-password.component';
import { SecurityComponent } from './core/modules/security/security.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      {
        path: '',
        component: DocumentationComponent,
        children: [
          { path: '', component: WelcomeComponent },
        ]
      },
      {
        path: 'security',
        component: SecurityComponent,
        children: [
          { path: 'auth', component: AuthComponent },
          { path: 'forgot-password', component: ForgotPasswordComponent },
          { path: 'reset-password', component: ResetPasswordComponent },
        ]
      }
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