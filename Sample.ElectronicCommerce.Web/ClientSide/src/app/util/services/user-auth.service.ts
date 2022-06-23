import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { TokenDTO } from '../entities/dto/token.dto';
import { UserDTO } from '../entities/dto/user.dto';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseUrl}/api/security/token`;
  }

  public authenticate(pEntity: UserDTO): Observable<any> {    
    return this.http.post<TokenDTO>(`${this.baseUrl}/auth`, pEntity);
  }

  public refresh(pId: string): Observable<any> {
    return this.http.get<TokenDTO>(`${this.baseUrl}/refresh/${pId}`);
  }
}