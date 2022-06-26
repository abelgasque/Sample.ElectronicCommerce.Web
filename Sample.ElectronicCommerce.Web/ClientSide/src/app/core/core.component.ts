import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/util/services/shared.service';
import { environment } from 'src/environments/environment';
import { LocalStorageService } from '../util/services/local-storage.service';

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.css']
})
export class CoreComponent implements OnInit {

  constructor(
    private localStorageService: LocalStorageService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void {
    this.loadApplication();
  }

  public loadApplication() {
    this.localStorageService.setAccessTokenBasic(environment.userName, environment.password);
    this.localStorageService.tokenBasic = this.localStorageService.getAccessTokenBasic();
    this.localStorageService.tokenBearer = this.localStorageService.getAccessTokenBearer();
    this.localStorageService.user = this.localStorageService.getUser();
    this.sharedService.getListMenu();
  }
}