import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { ReturnDTO } from 'src/app/shared/util/model';
import { CoreService } from 'src/app/core/core.service';

@Injectable({
  providedIn: 'root'
})
export class MailMessageService {

  private baseUrl: string;

  constructor(    
    private http: HttpClient,
    private coreService: CoreService,
  ) { 
    this.baseUrl =`${ environment.baseUrl }/MailMessage`; 
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
