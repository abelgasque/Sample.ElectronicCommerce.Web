import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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

import { SharedModule } from 'src/app/shared/shared.module';
import { UserRoutingModule } from 'src/app/core/modules/user/user-routing.module';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';
import { UserService } from 'src/app/core/modules/user/user.service';

import { UserComponent } from 'src/app/core/modules/user/user.component';
import { UserGridFilterComponent } from 'src/app/core/modules/user/components/user-grid-filter/user-grid-filter.component';
import { UserFormPersistComponent } from 'src/app/core/modules/user/components/user-form-persist/user-form-persist.component';

@NgModule({
  declarations: [
    UserComponent,
    UserGridFilterComponent,
    UserFormPersistComponent,
  ],
  imports: [
    UserRoutingModule,
    CommonModule,  
    ReactiveFormsModule.withConfig({warnOnNgModelWithFormControl: 'never'}),  
    FormsModule, 

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

    SharedModule,
  ],
  providers: [
    CoreService,
    SharedService,
    UserService,
  ]
})
export class UserModule { }
