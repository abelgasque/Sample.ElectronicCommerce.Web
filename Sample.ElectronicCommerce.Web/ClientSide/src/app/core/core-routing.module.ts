import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CoreComponent } from 'src/app/core/core.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    //canActivate: [AuthGuard],
    children: [
      
    ]
  }, 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoreRoutingModule { }
