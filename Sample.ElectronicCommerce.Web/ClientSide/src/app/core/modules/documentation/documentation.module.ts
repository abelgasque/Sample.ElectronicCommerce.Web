import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
    CommonModule
  ]
})
export class DocumentationModule { }
