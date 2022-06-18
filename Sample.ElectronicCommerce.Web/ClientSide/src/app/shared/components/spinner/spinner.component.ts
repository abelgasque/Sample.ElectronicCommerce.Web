import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/util/services/shared.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {

  constructor(public sharedService: SharedService) { }

  ngOnInit(): void { }
  
}