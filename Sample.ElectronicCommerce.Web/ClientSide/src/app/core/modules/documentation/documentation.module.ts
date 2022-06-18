import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


import { SharedModule } from 'src/app/shared/shared.module';
import { CoreService } from '../../core.service';
import { SharedService } from 'src/app/util/services/shared.service';
import { DocumentationComponent } from './documentation.component';
import { WelcomeComponent } from './layouts/welcome/welcome.component';
import { SwaggerComponent } from './layouts/swagger/swagger.component';

@NgModule({
  declarations: [
    DocumentationComponent,
    WelcomeComponent,
    SwaggerComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
  ],
  providers: [
    CoreService,
    SharedService,
  ]
})
export class DocumentationModule { }
