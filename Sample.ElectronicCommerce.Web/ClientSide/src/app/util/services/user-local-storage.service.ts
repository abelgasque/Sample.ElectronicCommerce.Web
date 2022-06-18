import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserLocalStorageService {


  constructor() { }

  public setAccessToken(pAccessToken: string) {    
    localStorage.setItem("access_token", pAccessToken);
  }

  public getAccessToken() {
    return localStorage.getItem("access_token");
  }

  public setUser(pUser: any) {
    localStorage.setItem("user", pUser);
  }

  public getUser() {
    return localStorage.getItem("user");
  }
}