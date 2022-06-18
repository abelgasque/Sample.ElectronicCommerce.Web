import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/util/services/shared.service';
import { JwtService } from 'src/app/util/services/jwt.service';

@Component({
  selector: 'app-mat-button-bar-user',
  templateUrl: './mat-button-bar-user.component.html',
  styleUrls: ['./mat-button-bar-user.component.css']
})
export class MatButtonBarUserComponent implements OnInit {

  constructor(    
    public jwtService: JwtService,
  ) { }

  ngOnInit(): void {
  }
}
