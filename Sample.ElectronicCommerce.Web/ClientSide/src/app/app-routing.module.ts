import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CoreComponent } from './core/core.component';
import { DocumentationComponent } from './core/modules/documentation/documentation.component';
import { WelcomeComponent } from './core/modules/documentation/layouts/welcome/welcome.component';

const routes: Routes = [
  {
    path: '',
    component: CoreComponent,
    children: [
      {
        path: '', 
        component: DocumentationComponent,
        children: [
          { path: '', component: WelcomeComponent },
        ]
      }
    ]
  },
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path: '**', redirectTo: 'page-not-found' },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }