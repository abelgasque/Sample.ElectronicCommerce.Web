import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';

import { CoreService } from 'src/app/core/core.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  public linkSwagger: string;

  constructor(private coreService: CoreService) { 
    this.linkSwagger =`${ environment.baseUrl }/swagger/index.html`; 
  }

  ngOnInit(): void {
  }

}
