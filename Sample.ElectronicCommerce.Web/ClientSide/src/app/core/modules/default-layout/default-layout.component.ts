import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.css']
})
export class DefaultLayoutComponent implements OnInit {

  constructor(public sharedService: SharedService) { }

  ngOnInit(): void { }

}
