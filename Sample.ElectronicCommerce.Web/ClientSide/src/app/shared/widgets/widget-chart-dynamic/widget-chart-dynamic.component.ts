import { Component, AfterViewInit, ViewChild, Input } from '@angular/core';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Color } from 'chartjs-plugin-datalabels/types/options';

import DataLabelsPlugin from 'chartjs-plugin-datalabels';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-widget-chart-dynamic',
  templateUrl: './widget-chart-dynamic.component.html',
  styleUrls: ['./widget-chart-dynamic.component.css']
})
export class WidgetChartDynamicComponent implements AfterViewInit {
  
  @Input() title: string = null;  
  @Input() hasBtnMenu: boolean = false;
  @Input() menuItems: MenuItem[] = [];  
  @Input() chartType: ChartType = 'bar';
  @Input() chartData: ChartData;

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    elements: { line: { tension: 0.4  } },
    scales: { x: {},  y: { min: 0 } },
    plugins: {
      legend: {
        display: true,
      },
      datalabels: {
        anchor: 'end',
        align: 'end'
      }
    }
  };  
  
  public chartPlugins = [ DataLabelsPlugin ];

  constructor() { }

  ngAfterViewInit(): void {
  }
}
