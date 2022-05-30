import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { PasswordModule } from 'primeng/password';
import { SelectButtonModule } from 'primeng/selectbutton';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';

import { MailRoutingModule } from './mail-routing.module';

import { MailService } from './mail.service';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { MailComponent } from 'src/app/core/modules/mail/mail.component';
import { MailFormPersistComponent } from 'src/app/core/modules/mail/components/mail-form-persist/mail-form-persist.component';
import { MailModelPersistComponent } from 'src/app/core/modules/mail/components/mail-model-persist/mail-model-persist.component';
import { MailGridFilterComponent } from 'src/app/core/modules/mail/components/mail-grid-filter/mail-grid-filter.component';

@NgModule({
  declarations: [
    MailComponent,
    MailFormPersistComponent,
    MailModelPersistComponent,
    MailGridFilterComponent
  ],
  imports: [
    MailRoutingModule,
    CommonModule,  
    RouterModule,  
    HttpClientModule,
    FormsModule,    
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),    

    MatCardModule,
    MatTabsModule,

    TableModule,
    ButtonModule,
    DialogModule,
    CheckboxModule,
    InputTextModule,
    InputMaskModule,
    PasswordModule,
    SelectButtonModule,
    MultiSelectModule,
    CalendarModule,    
  ],
  providers:[
    MailService,
    CoreService,
    SharedService,
  ]
})
export class MailModule { }
