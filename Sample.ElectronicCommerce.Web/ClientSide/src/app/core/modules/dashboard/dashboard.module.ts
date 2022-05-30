import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';

import { TableModule } from 'primeng/table';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

import { DashboardComponent } from 'src/app/core/modules/dashboard/dashboard.component';
import { DashboardLogAppComponent } from 'src/app/core/modules/dashboard/components/dashboard-log-app/dashboard-log-app.component';
import { DashboardMailBrokerComponent } from 'src/app/core/modules/dashboard/components/dashboard-mail-broker/dashboard-mail-broker.component';

@NgModule({
  declarations: [
    DashboardComponent,
    DashboardLogAppComponent,
    DashboardMailBrokerComponent,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,

    MatCardModule,

    TableModule,

    SharedModule,
  ]
})
export class DashboardModule { }
