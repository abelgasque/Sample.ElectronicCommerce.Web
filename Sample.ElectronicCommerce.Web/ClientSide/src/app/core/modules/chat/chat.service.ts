import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  
  private baseUrl: string;

  constructor(    
    private http: HttpClient,     
  ) {
    this.baseUrl =`${ environment.baseUrl }/Chat/Broker`;
  }

  public GetAll() : Observable<any>  {
    return this.http.get<ReturnDTO>(`${ this.baseUrl }/GetAll`);
  }
}
