import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';

import { ReturnDTO, TokenDTO, UserSession, UserWs } from 'src/app/shared/util/model';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/shared/shared.service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  private baseUrl: string;
  public hasUserAuth: boolean = false;
  public idUserSession: number = 0;
  public userSession: UserSession = null;

  constructor(      
    private router: Router,       
    private http: HttpClient,     
    private coreService: CoreService,    
    private jwtHelperService: JwtHelperService, 
    private sharedService: SharedService, 
  ) { 
    this.baseUrl =`${ environment.baseUrl }/Token`; 
  }

  public Authenticate(pEntity: UserWs) : Observable<any>  {
    return this.http.post<ReturnDTO>(`${ this.baseUrl }/Login`, pEntity);
  }

  public Refresh(pEntity: TokenDTO) : Observable<any>  {
    return this.http.post<ReturnDTO>(`${ this.baseUrl }/Refresh`, pEntity);
  } 

  public refreshUserSession(){       
    if(this.isValidUserSession()) {
      this.sharedService.openSpinner();
      let userSession: UserSession = this.getUserSession();
      let token: TokenDTO = new TokenDTO();
      token.idUserSession = userSession.id;
      token.accessToken = (userSession.refreshToken != null) ? userSession.refreshToken : userSession.accessToken;
      this.Refresh(token).subscribe({
        next: (returnDTO: ReturnDTO) => {
          if(returnDTO.isSuccess) {            
            this.authenticateUser(returnDTO.resultObject);
          }else{
            this.authenticateUser(null); 
          }
          this.sharedService.closeSpinner();
        },
        error: (error: any) => { 
          this.coreService.errorHandler(error);
        }        
      });
    }   
  }

  public authenticateUser(pEntity: UserSession) {    
    this.hasUserAuth = (pEntity != null && pEntity.isSuccess);
    if(pEntity != null){
      this.userSession = pEntity;      
      this.setUserSession(this.userSession);   
      let token: string = this.userSession.accessToken;
      if(this.hasUserAuth && this.userSession.refreshToken != null) {
        token = this.userSession.refreshToken;
      }
      this.setToken(token);
    }else{
      this.setUserSession(null);
      this.setToken(null);
    } 
  }

  public loggout() {      
    this.sharedService.closeSideBar();
    this.authenticateUser(null);
    this.router.navigate(['']);     
  }

  public hasRole(pRole: string) : boolean {     
    let hasRole = false;
    let userSession: UserSession = this.getUserSession();
    for (const role of userSession.user.roles) {      
      if (role.code == pRole) {
        return true;
      }
    }   
    return hasRole;
  }

  public hasAnyRole(roles) : boolean {    
    for (const role of roles) {      
      if (this.hasRole(role)) {
        return true;
      }
    }
    return false;
  }

  private setToken(token: string) {        
    localStorage.setItem("access_token", token);
  }
    
  private getToken() {
    return localStorage.getItem("access_token");
  }

  private decodeToken(pToken: string){
    return this.jwtHelperService.decodeToken(pToken);
  }
  
  private isTokenExpired(pToken: string){ 
    return this.jwtHelperService.isTokenExpired(pToken);
  }

  private setUserSession(pEntity: UserSession) {
    let dataObject: string = null;
    if(pEntity != null && pEntity.id > 0){
      dataObject = JSON.stringify(pEntity)
    }    
    localStorage.setItem("user_session", dataObject);  
  }

  public getUserSession() : UserSession {
    let dataObject: string = localStorage.getItem("user_session");
    return (dataObject != null && dataObject.length > 0) ? JSON.parse(dataObject) : null;
  }
    
  public isValidUserSession() {       
    let userSession: UserSession = this.getUserSession();
    return (userSession != null && userSession.id > 0);
  }

  public isValidToken() : boolean {
    let token: string = this.getToken();
    return (token != null && token.length > 0) ? (!this.isTokenExpired(token)) : false;
  }
}
