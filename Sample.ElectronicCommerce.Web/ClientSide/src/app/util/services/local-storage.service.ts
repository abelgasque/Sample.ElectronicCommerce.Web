import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {


  constructor() { }

  public setAccessTokenBasic(pUserName: string, pPassword: string) {    
    localStorage.setItem("access_token_basic", btoa(`${pUserName}:${pPassword}`));
  }

  public getAccessTokenBasic() {
    return localStorage.getItem("access_token_basic");
  }

  public setAccessTokenBearer(pToken: string) {    
    localStorage.setItem("access_token_bearer", pToken);
  }

  public getAccessTokenBearer() {
    return localStorage.getItem("access_token_bearer");
  }

  public setUser(pUser: any) {
    localStorage.setItem("user", pUser);
  }

  public getUser() {
    return localStorage.getItem("user");
  }
}