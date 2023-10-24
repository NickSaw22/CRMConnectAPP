import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { CrmChartDataServiceService } from 'app/services/crm-chart-data-service.service';
import * as Chartist from 'chartist';
import * as Highcharts from 'highcharts';
import exportingInit from 'highcharts/modules/exporting';
import exportDataInit from 'highcharts/modules/export-data';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private chartDataService: CrmChartDataServiceService,
    @Inject(ChangeDetectorRef) public cdRef: ChangeDetectorRef) { }
  startAnimationForLineChart(chart){
      let seq: any, delays: any, durations: any;
      seq = 0;
      delays = 80;
      durations = 500;

      chart.on('draw', function(data) {
        if(data.type === 'line' || data.type === 'area') {
          data.element.animate({
            d: {
              begin: 600,
              dur: 700,
              from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
              to: data.path.clone().stringify(),
              easing: Chartist.Svg.Easing.easeOutQuint
            }
          });
        } else if(data.type === 'point') {
              seq++;
              data.element.animate({
                opacity: {
                  begin: seq * delays,
                  dur: durations,
                  from: 0,
                  to: 1,
                  easing: 'ease'
                }
              });
          }
      });

      seq = 0;
  };
  startAnimationForBarChart(chart){
      let seq2: any, delays2: any, durations2: any;

      seq2 = 0;
      delays2 = 80;
      durations2 = 500;
      chart.on('draw', function(data) {
        if(data.type === 'bar'){
            seq2++;
            data.element.animate({
              opacity: {
                begin: seq2 * delays2,
                dur: durations2,
                from: 0,
                to: 1,
                easing: 'ease'
              }
            });
        }
      });

      seq2 = 0;
  };
  ngOnInit() {
      /* ----------==========     Daily Sales Chart initialization For Documentation    ==========---------- */

      const dataDailySalesChart: any = {
          labels: ['M', 'T', 'W', 'T', 'F', 'S', 'S'],
          series: [
              [12, 17, 7, 17, 23, 18, 38]
          ]
      };

     const optionsDailySalesChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0},
      }

      var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

      this.startAnimationForLineChart(dailySalesChart);


      /* ----------==========     Completed Tasks Chart initialization    ==========---------- */

      const dataCompletedTasksChart: any = {
          labels: ['12p', '3p', '6p', '9p', '12p', '3a', '6a', '9a'],
          series: [
              [230, 750, 450, 300, 280, 240, 200, 190]
          ]
      };

     const optionsCompletedTasksChart: any = {
          lineSmooth: Chartist.Interpolation.cardinal({
              tension: 0
          }),
          low: 0,
          high: 1000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
          chartPadding: { top: 0, right: 0, bottom: 0, left: 0}
      }

      var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

      // start animation for the Completed Tasks Chart - Line Chart
      this.startAnimationForLineChart(completedTasksChart);



      /* ----------==========     Emails Subscription Chart initialization    ==========---------- */
      this.LoadBarChart();
      this.getPieChartData();   
  }

  createPieChart(chartData) {
    exportingInit(Highcharts);
    exportDataInit(Highcharts);
  
    const options: Highcharts.Options = {
      chart: {
        type: 'pie',
      },
      title: {
        text: 'Contacts by Job Title',
      },
      plotOptions: {
        pie: {
          allowPointSelect: true,
          cursor: 'pointer',
          dataLabels: {
            enabled: true,
            format: '<b>{point.name}</b>: {point.y}',
          },
        },
      },
      series: [
        {
          type: 'pie',
          name: 'Job Titles',
          data: chartData,
        },
      ],
    };
    
    // Create the chart
    Highcharts.chart('contactsJobsChart', options);
  }
  

  getPieChartData(){
    this.chartDataService.getContactJobPieFata().subscribe((res)=>{
      const mappedData = res['label'].map((label, index) => ({
        name: label,
        y: res['series'][index]
      }));
      this.createPieChart(mappedData);
    },
    (error)=>{console.log(error)})
  }
  // createPieChart(chartData): void {
  //   const jobTitles = chartData.label;
  //   const contactCounts = chartData.series;
  //   const data = {
  //     labels: jobTitles,
  //     series: contactCounts,
  //   };
  
  //   const options = {
  //     donut: true, // Create a pie chart with a hole in the center for a donut chart.
  //     donutWidth: 60, // Adjust the size of the hole.
  //     donutSolid: true, // Solid donut instead of an outline.
  //   };

  //   const responsiveOptions: any[] = [
  //     ['screen and (max-width: 640px)', {
  //       seriesBarDistance: 5,
  //       axisX: {
  //         labelInterpolationFnc: function (value) {
  //           return value[0];
  //         }
  //       }
  //     }]
  //   ]
  
  //   var contactsJobsChart = new Chartist.Pie('#contactsJobsChart', data, options, responsiveOptions);
  //   //start animation for the Emails Subscription Chart
  //   this.startAnimationForBarChart(contactsJobsChart);
  //   this.cdRef && this.cdRef.detectChanges();
  // }
  
  LoadBarChart(){
    this.chartDataService.getOppBarChartData().subscribe(
      (res)=>{
        var datawebsiteViewsChart = {
          labels: res['label'],
          series: [
            res['series']    
          ]
        };
        var optionswebsiteViewsChart = {
            axisX: {
                showGrid: false
            },
            low: 0,
            high: Math.max(...res['series']),
            chartPadding: { top: 0, right: 5, bottom: 0, left: 0}
        };
        var responsiveOptions: any[] = [
          ['screen and (max-width: 640px)', {
            seriesBarDistance: 5,
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0];
              }
            }
          }]
        ];
        var websiteViewsChart = new Chartist.Bar('#websiteViewsChart', datawebsiteViewsChart, optionswebsiteViewsChart, responsiveOptions);
    
        //start animation for the Emails Subscription Chart
        this.startAnimationForBarChart(websiteViewsChart);
        this.cdRef && this.cdRef.detectChanges();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
