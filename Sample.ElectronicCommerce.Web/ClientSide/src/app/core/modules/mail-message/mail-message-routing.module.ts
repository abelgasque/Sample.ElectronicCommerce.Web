import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MailMessageComponent } from './mail-message.component';

const routes: Routes = [
  { path: '', component: MailMessageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MailMessageRoutingModule { }
