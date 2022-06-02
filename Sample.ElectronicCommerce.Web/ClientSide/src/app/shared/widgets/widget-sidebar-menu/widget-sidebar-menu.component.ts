import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-widget-sidebar-menu',
  templateUrl: './widget-sidebar-menu.component.html',
  styleUrls: ['./widget-sidebar-menu.component.css']
})
export class WidgetSidebarMenuComponent implements OnInit {

  constructor(public sharedService: SharedService,) { }

  ngOnInit(): void {
  }

}
