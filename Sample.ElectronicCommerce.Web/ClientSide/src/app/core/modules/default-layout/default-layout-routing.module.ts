import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './default-layout.component';

import { PageHomeComponent } from 'src/app/core/modules/default-layout/layouts/page-home/page-home.component';
import { PageNotFoundComponent } from 'src/app/core/modules/default-layout/layouts/page-not-found/page-not-found.component';

const routes: Routes = [
  { 
    path: '', 
    component: DefaultLayoutComponent,
    children: [
      { path: '', component: PageHomeComponent },  
      { path: 'page-not-found', component: PageNotFoundComponent },  
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DefaultLayoutRoutingModule { }
