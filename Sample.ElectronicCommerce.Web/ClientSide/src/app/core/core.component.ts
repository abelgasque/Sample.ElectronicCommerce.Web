import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/util/services/shared.service';
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
    this.localStorageService.setAccessTokenBasic("Sample", "code_sample");
    this.sharedService.getListMenu();
  }  
}