import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { environment } from 'src/environments/environment';
import { UserLeadDTO } from '../entities/dto/user-lead.dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.baseUrl}/api/security/user`;
  }

  public userLeadInsertAsync(pEntity: UserLeadDTO): Observable<any> {    
    return this.http.post<UserLeadDTO>(`${this.baseUrl}/lead`, pEntity);
  }
}