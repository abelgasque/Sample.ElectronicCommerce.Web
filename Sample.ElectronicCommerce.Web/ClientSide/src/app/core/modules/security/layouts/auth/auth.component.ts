import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { UserDTO } from 'src/app/util/entities/dto/UserDTO';
import { TokenDTO } from 'src/app/util/entities/dto/TokenDTO';

import { CoreService } from 'src/app/core/core.service';

import { SharedService } from 'src/app/util/services/shared.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  public form: FormGroup;
  public entity = new UserDTO();

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private coreService: CoreService,
    private sharedService: SharedService,
  ) { }

  ngOnInit(): void {
    this.setForm();
  }

  private setForm() {
    this.form = this.formBuilder.group({
      mail: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required]],
    });
  }

  public auth() {
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
