import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { AppComponent } from 'src/app/app.component';

@NgModule({
  declarations: [AppComponent],
  imports: [ 
    AppRoutingModule,        
    RouterModule,    
    CoreModule,
    SharedModule,    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
