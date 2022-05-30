import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { FlexLayoutModule } from "@angular/flex-layout";

import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';

import { SecurityRoutingModule } from './security-routing.module';

import { CoreService } from 'src/app/core/core.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';
import { SharedService } from 'src/app/shared/shared.service';

import { SecurityComponent } from 'src/app/core/modules/security/security.component';
import { LoginComponent } from 'src/app/core/modules/security/layouts/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';

export function tokenGetter() {
  return localStorage.getItem("access_token");
}

@NgModule({
  declarations: [
    SecurityComponent,
    LoginComponent
  ],
  imports: [
    SecurityRoutingModule,
    CommonModule,    
    RouterModule,  
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    FlexLayoutModule,
    
    MatIconModule,
    MatButtonModule,
    MatCardModule,

    InputTextModule,
    PasswordModule,    

    SharedModule,
  ],
  providers:[       
    CoreService,
    SecurityService,
    SharedService,
  ]
})
export class SecurityModule { }
