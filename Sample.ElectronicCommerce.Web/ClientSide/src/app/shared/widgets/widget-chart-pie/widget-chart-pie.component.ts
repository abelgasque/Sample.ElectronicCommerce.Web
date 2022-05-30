import { Component, AfterViewInit, ViewChild, Input } from '@angular/core';
import DatalabelsPlugin from 'chartjs-plugin-datalabels';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-widget-chart-pie',
  templateUrl: './widget-chart-pie.component.html',
  styleUrls: ['./widget-chart-pie.component.css']
})
export class WidgetChartPieComponent implements AfterViewInit {

  @Input() title: string = null;
  @Input() hasBtnMenu: boolean = false;
  @Input() menuItems: MenuItem[] = [];  
  @Input() chartData: ChartData; 

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;  
  
  public chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
        display: false
      },
      datalabels: {
        formatter: (value, ctx) => {
          const label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };

  public chartType: ChartType = 'pie';
  public chartPlugins = [ DatalabelsPlugin ];
  public chartLegend = true;

  constructor() { }

  ngAfterViewInit(): void {
  }  

  public chartClicked({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event: MouseEvent, active: {}[] }): void {
    console.log(event, active);
  }
}
