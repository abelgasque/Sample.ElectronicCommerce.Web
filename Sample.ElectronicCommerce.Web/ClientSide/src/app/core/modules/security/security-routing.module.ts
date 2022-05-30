import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './layouts/login/login.component';
import { SecurityComponent } from './security.component';

const routes: Routes = [
  { 
    path: '', 
    component: SecurityComponent,
    children: [
      { path: 'auth', component: LoginComponent },
    ] 
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecurityRoutingModule { }
