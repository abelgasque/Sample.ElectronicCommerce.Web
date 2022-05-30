import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { CoreService } from 'src/app/core/core.service';
import { ReturnDTO } from 'src/app/shared/util/model';

@Injectable({
  providedIn: 'root'
})
export class UserSessionService {

  private baseUrl: string;

  constructor(    
    private http: HttpClient,
    private coreService: CoreService,
  ) 
  { 
    this.baseUrl =`${ environment.baseUrl }/UserSession`; 
  }
  
  public Insert(pEntity: any) : Observable<any> {
    return this.http.post<Promise<ReturnDTO>>(`${this.baseUrl}/InsertAsync`, pEntity);
  }

  public Update(pEntity: any) : Observable<any> {
    return this.http.put<Promise<ReturnDTO>>(`${this.baseUrl}/UpdateAsync`, pEntity);
  }

  public GetById(pId: number) : Observable<any> {
    return this.http.get<Promise<ReturnDTO>>(`${this.baseUrl}/GetById/${pId}`);
  }

  public GetAll(pIsActive: boolean) : Observable<any> {
    let endpoint: string = `${this.baseUrl}/GetAll`;
    if(pIsActive != null){
      endpoint += `?pIsActive=${pIsActive}`;
    }
    return this.http.get<Promise<ReturnDTO>>(endpoint);
  }
}
