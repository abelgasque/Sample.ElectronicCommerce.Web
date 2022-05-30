import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';

import { CoreService } from 'src/app/core/core.service';

@Component({
  selector: 'app-page-home',
  templateUrl: './page-home.component.html',
  styleUrls: ['./page-home.component.css']
})
export class PageHomeComponent implements OnInit {

  public linkSwagger: string;

  constructor(private coreService: CoreService) { 
    this.linkSwagger =`${ environment.baseUrl }/swagger/index.html`; 
  }

  ngOnInit(): void {
  }

}
