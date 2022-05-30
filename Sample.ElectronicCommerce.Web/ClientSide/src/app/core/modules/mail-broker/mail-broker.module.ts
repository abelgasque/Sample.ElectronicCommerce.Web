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

import { MailBrokerRoutingModule } from './mail-broker-routing.module';

import { MailBrokerService } from './mail-broker.service';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { MailBrokerComponent } from 'src/app/core/modules/mail-broker/mail-broker.component';
import { MailBrokerGridFilterComponent } from 'src/app/core/modules/mail-broker/components/mail-broker-grid-filter/mail-broker-grid-filter.component';
import { MailBrokerModelPersistComponent } from 'src/app/core/modules/mail-broker/components/mail-broker-model-persist/mail-broker-model-persist.component';
import { MailBrokerFormPersistComponent } from 'src/app/core/modules/mail-broker/components/mail-broker-form-persist/mail-broker-form-persist.component';

@NgModule({
  declarations: [
    MailBrokerComponent,
    MailBrokerGridFilterComponent,
    MailBrokerModelPersistComponent,
    MailBrokerFormPersistComponent,
  ],
  imports: [
    MailBrokerRoutingModule,
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
    MailBrokerService,
    CoreService,
    SharedService,
  ]
})
export class MailBrokerModule { }
