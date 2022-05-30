import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import * as moment from 'moment';

import { SharedService } from '../shared/shared.service';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CoreService 
{
  public listOptionYesOrNo: any[] = [
    { name: 'Sim', value: true },
    { name: 'Não', value: false },
  ];

  constructor(
    private sharedService: SharedService,
  ) { }

  public errorHandler(error: any){    
    let msg: string;
    if (typeof error === 'string') {
      msg = error;
    } else if (error instanceof HttpErrorResponse && error.status >= 400 && error.status <= 499) {
      msg = 'Ocorreu um erro ao processar a sua solicitação';

      if (error.status === 403) {
        msg = 'Você não tem permissão para executar esta ação';
      }

      try {
        msg = error.error[0].mensagemUsuario;
      } 
      catch (e) {  }
      console.error('Ocorreu um erro', error);
    } else if (error.status === 404){
      msg = error.error.message;
    } else {
      msg = 'Erro ao processar serviço remoto. Tente novamente.';
      console.log('Ocorreu um erro', error);
    }    
    this.sharedService.closeSpinner();
    this.sharedService.showMessageError(msg);
  }

  public formatDatePtBr(data: string) {    
    return moment(data).format("DD/MM/YYYY HH:mm:ss");
  }
  
  public validField(control: AbstractControl) {    
    return !control.valid && control.touched;
  }

  public aplicaCssErro(control: AbstractControl) {
    return {
      'ng-invalid': this.validField(control),
      'ng-dirty': this.validField(control),
      'color-red': this.validField(control)
    };
  }
} 
