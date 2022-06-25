import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { UserDTO } from 'src/app/util/entities/dto/user.dto';
import { TokenDTO } from 'src/app/util/entities/dto/token.dto';

import { CoreService } from 'src/app/core/core.service';
import { SharedService } from 'src/app/util/services/shared.service';
import { UserAuthService } from 'src/app/util/services/user-auth.service';
import { LocalStorageService } from 'src/app/util/services/local-storage.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  public form: FormGroup;
  public entity = new UserDTO();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private coreService: CoreService,
    private userAuthService: UserAuthService,
    private localStorageService: LocalStorageService,
    public sharedService: SharedService,
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
    if ((!this.entity.mail) || (!this.entity.password)) {
      this.sharedService.showMessageWarn("É necessário preencher todos os campos do formulário!");
    } else {
      this.sharedService.openSpinner();
      this.userAuthService.authenticate(this.entity).subscribe({
        next: (resp: TokenDTO) => {
          this.localStorageService.setAccessTokenBearer(resp.access_token);
          this.localStorageService.setUser(resp.access_token);
          this.router.navigate(['']);
          this.sharedService.closeSpinner();
        },
        error: (error: any) => {
          this.localStorageService.setAccessTokenBearer(null);
          this.localStorageService.setUser(null);
          this.coreService.errorHandler(error);
        }
      });
    }
  }
}