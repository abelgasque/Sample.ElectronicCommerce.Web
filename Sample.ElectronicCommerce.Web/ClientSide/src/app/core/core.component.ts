import { Component, OnInit } from '@angular/core';

import { SharedService } from '../shared/shared.service';

@Component({
  selector: 'app-core',
  templateUrl: './core.component.html',
  styleUrls: ['./core.component.css']
})
export class CoreComponent implements OnInit {

  constructor(
    public sharedService: SharedService,
  ) {  }

  ngOnInit(): void {}

  public loadApplication() {
    this.sharedService.getListMenu();
  }
}