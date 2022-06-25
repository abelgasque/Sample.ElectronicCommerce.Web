import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  
  constructor(
    private jwtHelperService: JwtHelperService,
    private localStorageService: LocalStorageService,
  ) { }

  public decodeToken(pToken: string) {
    return this.jwtHelperService.decodeToken(pToken);
  }

  public isTokenExpired(pToken: string) {
    return this.jwtHelperService.isTokenExpired(pToken);
  }

  public getTokenExpirationDate(pToken: string) {
    return this.jwtHelperService.getTokenExpirationDate(pToken);
  }

  public isValidToken(): boolean {
    let token: string = this.localStorageService.getAccessTokenBearer();
    return (token != null && token.length > 0) ? (!this.isTokenExpired(token)) : false;
  }

  public validateRole(pRoles: string[]): boolean {
    let hasRole = false;
    // let userSession: UserSessionEntity = this.getUserSession();
    // for (const role of userSession.roles) {
    //   if (role.code == pRole) {
    //     return true;
    //   }
    // }
    return hasRole;
  }

  public validateAllRoles(roles): boolean {
    let result: boolean = false;
    for (const role of roles) {
      if (this.validateRole(role)) {
        result = true;
      }
    }
    return result;
  }
}