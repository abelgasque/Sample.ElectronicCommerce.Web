import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LogAppComponent } from './log-app.component';

const routes: Routes = [
  { path: '', component: LogAppComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LogAppRoutingModule { }
