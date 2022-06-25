import { Component, OnInit } from '@angular/core';

import { SharedService } from 'src/app/util/services/shared.service';
import { JwtService } from 'src/app/util/services/jwt.service';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/util/services/local-storage.service';

@Component({
  selector: 'app-mat-button-bar-user',
  templateUrl: './mat-button-bar-user.component.html',
  styleUrls: ['./mat-button-bar-user.component.css']
})
export class MatButtonBarUserComponent implements OnInit {

  constructor(
    private router: Router,
    public localStorageService: LocalStorageService,
    public jwtService: JwtService,
    public sharedService: SharedService,
  ) { }

  ngOnInit(): void { }

  loggout() {
    this.localStorageService.setAccessTokenBearer(null);
    this.localStorageService.setUser(null);
    this.router.navigate(['']);
  }
}