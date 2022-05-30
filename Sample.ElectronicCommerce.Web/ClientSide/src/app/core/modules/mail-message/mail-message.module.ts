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

import { MailMessageRoutingModule } from './mail-message-routing.module';

import { MailMessageService } from './mail-message.service';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { MailMessageComponent } from 'src/app/core/modules/mail-message/mail-message.component';
import { MailMessageGridFilterComponent } from 'src/app/core/modules/mail-message/components/mail-message-grid-filter/mail-message-grid-filter.component';
import { MailMessageModelPersistComponent } from 'src/app/core/modules/mail-message/components/mail-message-model-persist/mail-message-model-persist.component';
import { MailMessageFormPersistComponent } from 'src/app/core/modules/mail-message/components/mail-message-form-persist/mail-message-form-persist.component';

@NgModule({
  declarations: [
    MailMessageComponent,
    MailMessageGridFilterComponent,
    MailMessageModelPersistComponent,
    MailMessageFormPersistComponent,
  ],
  imports: [    
    MailMessageRoutingModule,
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
    MailMessageService,
    CoreService,
    SharedService,
  ]
})
export class MailMessageModule { }
