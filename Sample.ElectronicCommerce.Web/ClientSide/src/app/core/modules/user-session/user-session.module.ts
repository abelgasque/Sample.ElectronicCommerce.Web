import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatTabsModule } from '@angular/material/tabs';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { SelectButtonModule } from 'primeng/selectbutton';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';
import { TableModule } from 'primeng/table';
import { InputMaskModule } from 'primeng/inputmask';
import { CheckboxModule } from 'primeng/checkbox';
import { DialogModule } from 'primeng/dialog';

import { UserSessionRoutingModule } from './user-session-routing.module';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { UserSessionService } from 'src/app/core/modules/user-session/user-session.service';

import { UserSessionComponent } from 'src/app/core/modules/user-session/user-session.component';
import { UserSessionGridFilterComponent } from 'src/app/core/modules/user-session/components/user-session-grid-filter/user-session-grid-filter.component';
import { UserSessionModelPersistComponent } from 'src/app/core/modules/user-session/components/user-session-model-persist/user-session-model-persist.component';
import { UserSessionFormPersistComponent } from 'src/app/core/modules/user-session/components/user-session-form-persist/user-session-form-persist.component';

@NgModule({
  declarations: [
    UserSessionComponent,
    UserSessionGridFilterComponent,
    UserSessionModelPersistComponent,
    UserSessionFormPersistComponent,
  ],
  imports: [
    UserSessionRoutingModule,
    CommonModule,
    RouterModule,
    FormsModule, 
    HttpClientModule,    
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),    

    MatCardModule,
    MatTabsModule,
    
    TableModule,
    ButtonModule,
    CheckboxModule,
    InputTextModule,
    InputMaskModule,
    PasswordModule,
    SelectButtonModule,
    MultiSelectModule,
    CalendarModule,    
    DialogModule,
  ],
  providers: [
    CoreService,
    SharedService,
    UserSessionService,
  ]
})
export class UserSessionModule { }
