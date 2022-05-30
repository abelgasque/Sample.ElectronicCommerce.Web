import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs';

import { SecurityService } from 'src/app/core/modules/security/security.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,    
    private securityService: SecurityService,
  ) { }

  public canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let isValid = false;      
    if(!this.securityService.isValidUserSession()) {            
      this.router.navigate(['/security/auth']);
    }else if (!this.securityService.isValidToken()) {            
      this.securityService.refreshUserSession();
    } else if (next.data.roles && !this.securityService.hasAnyRole(next.data.roles)) {             
      this.router.navigate(['/page-not-authorized']);      
    } else {      
      isValid = true;
    }  
    return isValid;
  }
}