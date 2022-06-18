import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { MatSidenavModule } from '@angular/material/sidenav';

import { JwtModule } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';

import { CoreRoutingModule } from 'src/app/core/core-routing.module';
import { CoreService } from 'src/app/core/core.service';
import { CoreComponent } from 'src/app/core/core.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SharedService } from 'src/app/util/services/shared.service';
import { DocumentationModule } from 'src/app/core/modules/documentation/documentation.module';
import { SecurityModule } from 'src/app/core/modules/security/security.module';
import { UserModule } from 'src/app/core/modules/user/user.module';
import { UserLeadModule } from 'src/app/core/modules/user-lead/user-lead.module';
import { UserRoleModule } from 'src/app/core/modules/user-role/user-role.module';

export function tokenGetter() {
  return localStorage.getItem("access_token");
}

@NgModule({
  declarations: [
    CoreComponent
  ],
  imports: [       
    CommonModule,     
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),    
    BrowserAnimationsModule,    
    RouterModule,    
    HttpClientModule,
    
    MatSidenavModule,
    
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.tokenWhitelistedDomains,
        disallowedRoutes: environment.tokenBlacklistedRoutes
      }
    }),

    CoreRoutingModule,    
    DocumentationModule,
    SecurityModule,
    SharedModule,
    UserModule,
    UserLeadModule,
    UserRoleModule,
  ],
  providers: [    
    { provide: LOCALE_ID, useValue: 'pt-br' },
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    CoreService,
    SharedService,
  ],
})
export class CoreModule { }
