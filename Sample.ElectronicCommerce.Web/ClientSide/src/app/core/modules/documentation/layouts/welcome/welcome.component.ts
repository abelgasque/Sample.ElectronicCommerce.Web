import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  public linkSwagger: string;

  constructor() { 
    this.linkSwagger =`${ environment.baseUrl }/swagger/index.html`; 
  }

  ngOnInit(): void {
  }

}
