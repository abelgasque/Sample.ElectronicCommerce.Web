import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/util/services/shared.service';
import { DocumentationComponent } from 'src/app/core/modules/documentation/documentation.component';
import { WelcomeComponent } from 'src/app/core/modules/documentation/layouts/welcome/welcome.component';

@NgModule({
  declarations: [
    DocumentationComponent,
    WelcomeComponent
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
