import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserSessionComponent } from './user-session.component';

const routes: Routes = [
  { path: '', component: UserSessionComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserSessionRoutingModule { }
