import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserLocalStorageService } from './user-local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class JwtService {

  public token: string;
  public user: any;

  constructor(
    private jwtHelperService: JwtHelperService,
    private userLocalStorageService: UserLocalStorageService,
  ) {
    let token = this.userLocalStorageService.getAccessToken();
    this.token = (token) ? token : null;
    let user = this.userLocalStorageService.getUser();
    this.user = (user) ? user : null;
  }

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
    let token: string = this.userLocalStorageService.getAccessToken();
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