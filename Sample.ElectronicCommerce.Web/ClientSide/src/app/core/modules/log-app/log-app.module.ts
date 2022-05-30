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

import { LogAppRoutingModule } from './log-app-routing.module';

import { LogAppService } from './log-app.service';
import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { LogAppComponent } from 'src/app/core/modules/log-app/log-app.component';
import { LogAppFormPersistComponent } from 'src/app/core/modules/log-app/components/log-app-form-persist/log-app-form-persist.component';
import { LogAppGridFilterComponent } from 'src/app/core/modules/log-app/components/log-app-grid-filter/log-app-grid-filter.component';
import { LogAppModelPersistComponent } from 'src/app/core/modules/log-app/components/log-app-model-persist/log-app-model-persist.component';

@NgModule({
  declarations: [
    LogAppComponent,
    LogAppFormPersistComponent,
    LogAppGridFilterComponent,
    LogAppModelPersistComponent
  ],
  imports: [
    LogAppRoutingModule,
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
    LogAppService,
    CoreService,
    SharedService,
  ]
})
export class LogAppModule { }
