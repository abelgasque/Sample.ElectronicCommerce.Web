import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';

@Injectable({
  providedIn: 'root'
})
export class MailService {

  private baseUrl: string;

  constructor(    
    private http: HttpClient,
  ) { 
    this.baseUrl =`${ environment.baseUrl }/Mail`; 
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
