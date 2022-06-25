import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  public tokenBasic: string = null;
  public tokenBearer: string = null;
  public user: any = null;

  constructor() { }

  public setAccessTokenBasic(pUserName: string, pPassword: string) {
    this.tokenBasic = btoa(`${pUserName}:${pPassword}`);
    localStorage.setItem("access_token_basic", this.tokenBasic);
  }

  public getAccessTokenBasic() {
    return localStorage.getItem("access_token_basic");
  }

  public setAccessTokenBearer(pToken: string) {
    this.tokenBearer = pToken;
    localStorage.setItem("access_token_bearer", this.tokenBearer);
  }

  public getAccessTokenBearer() {
    return localStorage.getItem("access_token_bearer");
  }

  public setUser(pUser: any) {
    this.user = pUser;
    localStorage.setItem("user", this.user);
  }

  public getUser() {
    return localStorage.getItem("user");
  }
}