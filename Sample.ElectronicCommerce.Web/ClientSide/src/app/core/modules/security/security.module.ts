import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SecurityService } from './security.service';

import { SecurityComponent } from './security.component';
import { AuthComponent } from './layouts/auth/auth.component';
import { ForgotPasswordComponent } from './layouts/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './layouts/reset-password/reset-password.component';

@NgModule({
  declarations: [
    SecurityComponent,
    AuthComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
  ],
  providers: [
    SecurityService
  ]
})
export class SecurityModule { }
