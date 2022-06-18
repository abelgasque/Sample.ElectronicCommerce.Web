import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

import { UserDTO } from 'src/app/shared/util/EntitiesDTO/UserDTO';
import { ReturnDTO } from 'src/app/shared/util/EntitiesDTO/ReturnDTO';

import { CoreService } from 'src/app/core/core.service';
import { SecurityService } from 'src/app/core/modules/security/security.service';

import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  public entity = new UserDTO();

  constructor(
    private router: Router,
    private coreService: CoreService,
    private securityService: SecurityService,
    private sharedService: SharedService,
  ) { }

  ngOnInit(): void {
  }

  public authenticate(f: NgForm) {
    if ((this.entity.mail.length <= 0) || (this.entity.password.length <= 0)) {
      this.sharedService.showMessageWarn("Preencha o formulário corretamente!");
    } else {
      // this.sharedService.openSpinner();
      // this.securityService.Authenticate(this.entity).subscribe({
      //   next: (returnDTO: ReturnDTO) => {
      //     if (returnDTO.isSuccess) {
      //       this.securityService.authenticateUser(returnDTO.resultObject);
      //       this.router.navigate(['']);
      //     } else {
      //       this.securityService.authenticateUser(null);
      //       this.sharedService.showMessageWarn(returnDTO.deMessage);
      //     }
      //     this.sharedService.closeSpinner();
      //   },
      //   error: (error: any) => {
      //     this.coreService.errorHandler(error);
      //   }
      // });
    }
  }
}
