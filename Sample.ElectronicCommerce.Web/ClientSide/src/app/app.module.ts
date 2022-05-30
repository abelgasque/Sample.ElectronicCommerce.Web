import { LOCALE_ID, NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { JwtModule } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';

import { AppRoutingModule } from 'src/app/app-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';

import { CoreService } from 'src/app/core/core.service';

import { AppComponent } from 'src/app/app.component';
import { SharedService } from './shared/shared.service';
import { SecurityService } from './core/modules/security/security.service';

export function tokenGetter() {
  return localStorage.getItem("access_token");
}

@NgModule({
  declarations: [AppComponent],
  imports: [ 
    AppRoutingModule,        
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),    
    BrowserAnimationsModule,    
    RouterModule,    
    HttpClientModule,
    
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.tokenWhitelistedDomains,
        disallowedRoutes: environment.tokenBlacklistedRoutes
      }
    }),
    
    CoreModule,
    SharedModule,    
  ],
  providers: [    
    { provide: LOCALE_ID, useValue: 'pt-br' },
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    CoreService,
    SharedService,
    SecurityService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
