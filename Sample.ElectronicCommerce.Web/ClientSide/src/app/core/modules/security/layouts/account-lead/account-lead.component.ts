import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/core/core.service';

import { UserLeadDTO } from 'src/app/util/entities/dto/user-lead.dto';
import { SharedService } from 'src/app/util/services/shared.service';
import { UserService } from 'src/app/util/services/user.service';

@Component({
  selector: 'app-account-lead',
  templateUrl: './account-lead.component.html',
  styleUrls: ['./account-lead.component.css']
})
export class AccountLeadComponent implements OnInit {

  public form: FormGroup;
  public entity = new UserLeadDTO();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private coreService: CoreService,
    private userService: UserService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void {
    this.setForm();
  }

  private setForm() {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      mail: [null, [Validators.required, Validators.email]],
      phone: [null, [Validators.required]],
    });
  }

  public insert() {
    if (!this.form.valid) {
      this.sharedService.showMessageWarn("Formulário inválido!");
    } else {
      this.sharedService.openSpinner();
      this.userService.userLeadInsertAsync(this.entity).subscribe({
        next: (resp: any) => {
          this.router.navigate(['/security/reset/password']);
          this.sharedService.closeSpinner();
        },
        error: (error: any) => {
          this.coreService.errorHandler(error);
        }
      });
    }
  }
}