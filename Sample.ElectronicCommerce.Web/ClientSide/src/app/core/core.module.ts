import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { MatSidenavModule } from '@angular/material/sidenav';

import { JwtModule } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';

import { CoreService } from 'src/app/core/core.service';
import { CoreComponent } from 'src/app/core/core.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SharedService } from 'src/app/util/services/shared.service';
import { DocumentationModule } from 'src/app/core/modules/documentation/documentation.module';
import { SecurityModule } from 'src/app/core/modules/security/security.module';
import { UserModule } from 'src/app/core/modules/user/user.module';
import { UserRoleModule } from 'src/app/core/modules/user-role/user-role.module';

const listBasicAuth = [
  "api/security/token/auth"
];

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
        authScheme: (request) => {          
          for (let i = 0; i < listBasicAuth.length; i++) {
            if (request.url.includes(listBasicAuth[i])) {
              return "Basic ";
            }
          }
          return "Bearer ";
        },
        tokenGetter: (request) => {
          for (let i = 0; i < listBasicAuth.length; i++) {
            if (request.url.includes(listBasicAuth[i])) {
              return localStorage.getItem("access_token_basic");
            }
          }

          return localStorage.getItem("access_token_bearer");
        },
        allowedDomains: environment.tokenWhitelistedDomains,
        disallowedRoutes: environment.tokenBlacklistedRoutes
      }
    }),

    DocumentationModule,
    SecurityModule,
    SharedModule,
    UserModule,
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
