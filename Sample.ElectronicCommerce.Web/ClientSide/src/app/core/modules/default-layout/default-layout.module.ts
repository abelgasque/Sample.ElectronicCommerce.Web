import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { DefaultLayoutRoutingModule } from './default-layout-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

import { CoreService } from '../../core.service';
import { SharedService } from 'src/app/shared/shared.service';

import { DefaultLayoutComponent } from './default-layout.component';
import { PageHomeComponent } from './layouts/page-home/page-home.component';
import { PageMaintenanceComponent } from './layouts/page-maintenance/page-maintenance.component';
import { PageNotAuthorizedComponent } from './layouts/page-not-authorized/page-not-authorized.component';
import { PageNotFoundComponent } from './layouts/page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    DefaultLayoutComponent,  
    PageHomeComponent,
    PageMaintenanceComponent,
    PageNotAuthorizedComponent,
    PageNotFoundComponent,  
  ],
  imports: [
    DefaultLayoutRoutingModule,
    CommonModule,    
    RouterModule,
    
    SharedModule,
  ],
  providers:[ 
    CoreService,
    SharedService, 
  ]
})
export class DefaultLayoutModule { }
