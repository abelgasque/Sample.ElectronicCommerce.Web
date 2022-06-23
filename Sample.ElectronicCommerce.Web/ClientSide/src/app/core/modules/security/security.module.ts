import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { FlexLayoutModule } from "@angular/flex-layout";

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { UserAuthService } from 'src/app/util/services/user-auth.service';
import { CoreService } from 'src/app/core/core.service';
import { LocalStorageService } from 'src/app/util/services/local-storage.service';
import { SharedService } from 'src/app/util/services/shared.service';

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
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),

    FlexLayoutModule,

    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [
    CoreService,
    UserAuthService,
    LocalStorageService,
    SharedService,
  ]
})
export class SecurityModule { }
