import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';

import { NgChartsModule } from 'ng2-charts';

import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MenuModule } from 'primeng/menu';
import { MessageModule } from 'primeng/message';

import { CoreService } from '../core/core.service';
import { SharedService } from '../util/services/shared.service';

import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { SpinnerLoadingComponent } from './components/spinner-loading/spinner-loading.component';
import { MessagesComponent } from './components/messages/messages.component';

import { WidgetChartDynamicComponent } from './widgets/widget-chart-dynamic/widget-chart-dynamic.component';
import { WidgetChartPieComponent } from './widgets/widget-chart-pie/widget-chart-pie.component';
import { MatButtonBarUserComponent } from './components/mat-button-bar-user/mat-button-bar-user.component';
import { MatButtonBarSystemComponent } from './components/mat-button-bar-system/mat-button-bar-system.component';
import { MatButtonBarSearchComponent } from './components/mat-button-bar-search/mat-button-bar-search.component';
import { WidgetSidebarChatComponent } from './widgets/widget-sidebar-chat/widget-sidebar-chat.component';
import { WidgetSidebarMenuComponent } from './widgets/widget-sidebar-menu/widget-sidebar-menu.component';
import { WidgetSidebarUserComponent } from './widgets/widget-sidebar-user/widget-sidebar-user.component';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    SpinnerComponent,
    SpinnerLoadingComponent,
    MessagesComponent,

    WidgetChartDynamicComponent,
    WidgetChartPieComponent,
    MatButtonBarUserComponent,
    MatButtonBarSystemComponent,
    MatButtonBarSearchComponent,
    WidgetSidebarChatComponent,
    WidgetSidebarMenuComponent,
    WidgetSidebarUserComponent,
  ],
  imports: [
    CommonModule,    
    RouterModule,
    FormsModule,
    ReactiveFormsModule,

    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatMenuModule,
    MatListModule,
    MatCardModule,
    
    NgChartsModule,

    ButtonModule,
    ProgressSpinnerModule,
    ToastModule,
    MenuModule,
    MessageModule,
  ],
  exports:[
    NavbarComponent,
    FooterComponent,
    SpinnerComponent, 
    SpinnerLoadingComponent,   
    MessagesComponent,
    
    WidgetChartDynamicComponent,
    WidgetChartPieComponent,
    WidgetSidebarChatComponent,
    WidgetSidebarMenuComponent,
    WidgetSidebarUserComponent,
  ],
  providers:[
    CoreService,
    SharedService,
    MessageService,
  ]
})
export class SharedModule { }
