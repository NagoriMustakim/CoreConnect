import { InternalProgramsService } from '../../service/internal-programs.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzProgressModule } from 'ng-zorro-antd/progress';
import { ManagementDocumentConstants } from '../../../management/constants/ManagementDocumentConstants';
import { BasicDetailService } from '../../service/basic-detail.service';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { Router } from '@angular/router';
import { HomeService } from '../../service/home.service';
import { concatMap, min, Subscription, catchError, of } from 'rxjs';
import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle,
  NgApexchartsModule,
  ApexStroke,
  ApexTooltip,
  ApexDataLabels,
  ApexLegend,
} from 'ng-apexcharts';
import ApexCharts from 'apexcharts';
import { EventEmitterService } from '../../service/event-emitter.service';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  stroke: ApexStroke;
  tooltip: ApexTooltip;
  title: ApexTitleSubtitle;
  dataLabels: ApexDataLabels;
  legend: ApexLegend;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  fill: ApexFill;
};

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NzButtonModule,
    NzCardModule,
    NzTabsModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzIconModule,
    NzFlexModule,
    NzSpaceModule,
    NzImageModule,
    NzModalModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzUploadModule,
    CommonModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzTagModule,
    NzInputNumberModule,
    NzGridModule,
    NzProgressModule,
    NgApexchartsModule,
    NzListModule,
    NzStatisticModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', '/src/styles.css'],
})
export class HomeComponent implements OnInit {
  hours: number = new Date().getHours();
  greeting: string =
    this.hours < 12
      ? AdminDocumentConstants.GOOD_MORNING_MSG
      : this.hours >= 12 && this.hours <= 18
      ? AdminDocumentConstants.GOOD_AFTERNOON_MSG
      : AdminDocumentConstants.GOOD_EVENING_MSG;
  basicDetails = {} as IBasicDetail;
  dashboardDetails: any;
  public chartOptions: Partial<ChartOptions> | any;
  public barChartOptions: Partial<ChartOptions> | any;
  resetSubscription!: Subscription;
  @ViewChild(AdminDocumentConstants.CHART)
  chart!: ChartComponent;
  @ViewChild(AdminDocumentConstants.BARCHART)
  barChart!: ChartComponent;
  internalPrograms: any[] = [];

  constructor(
    private basicDetailService: BasicDetailService,
    private router: Router,
    private homeService: HomeService,
    private eventEmitterService: EventEmitterService,
    private internalProgramService: InternalProgramsService
  ) {
    this.basicDetailService
      .getAdminBasicDetails()
      .pipe(
        concatMap((response) => {
          this.basicDetails = response;
          return this.homeService.getDashboardDetails();

        })
        )
        .pipe(
          concatMap((response:any) => {
            this.dashboardDetails = response;
            this.dashboardDetails?.totalApprovedNominations.push(
              this.dashboardDetails?.giftInternalProgram
            );

            this.setChartOptions();

          return this.internalProgramService.getInternalPrograms();
        })
      )
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.internalPrograms = res.result;
        console.log("ip", this.internalPrograms);

      });
  }

  ngOnInit() {
    this.resetSubscription = this.eventEmitterService
      .getResetObservable()
      .subscribe(() => {
        this.resetCharts();
      });
  }
   getRandomColor(): string {
    const colors: string[] = ['blue', 'green', 'purple', 'magenta'];
    const randomIndex: number = Math.floor(Math.random() * colors.length);

    return colors[randomIndex];
  }
  goto(){
    this.router.navigate(['dashboard/internal-programs'])
  }
  setChartOptions() {
    this.chartOptions = {
      series: this.dashboardDetails?.totalApprovedNominations,
      chart: {
        height: 400,
        type: AdminDocumentConstants.AREA,
        fontFamily: AdminDocumentConstants.POPPINS_SANS_SERIF,

        toolbar: {
          export: {
            csv: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
              columnDelimiter: AdminDocumentConstants.COMMA,
              headerCategory: AdminDocumentConstants.DATE,
              headerValue: AdminDocumentConstants.COUNT,
            },
            svg: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
            },
            png: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
            },
          },
        },
      },
      title: {
        text: AdminDocumentConstants.INTERNAL_PROGRAM,
        style: {
          fontSize: AdminDocumentConstants.FONT_SIZE_20PX,
          fontWeight: AdminDocumentConstants.NORMAL,
        },
      },
      stroke: {
        curve: AdminDocumentConstants.SMOOTH,
      },
      tooltip: {
        followCursor: true,
        x: {
          show: true,
          format: AdminDocumentConstants.YYYY,
        },
      },
      dataLabels: {
        enabled: false,
      },
      xaxis: {
        type: AdminDocumentConstants.DATETIME,
        labels: {
          datetimeFormatter: {
            year: AdminDocumentConstants.YYYY,
          },
        },
      },
      legend: {
        show: true,
      },
    };

    this.barChartOptions = {
      series: [
        {
          name: AdminDocumentConstants.GIFT,
          data: this.dashboardDetails?.gitfPromotionsPercentage,
        },
      ],
      chart: {
        height: 400,
        type: AdminDocumentConstants.BAR,
        fontFamily: AdminDocumentConstants.POPPINS_SANS_SERIF,
        toolbar: {
          export: {
            csv: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
              columnDelimiter: AdminDocumentConstants.COMMA,
              headerCategory: AdminDocumentConstants.DATE,
              headerValue: AdminDocumentConstants.COUNT,
            },
            svg: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
            },
            png: {
              filename: AdminDocumentConstants.INTERNAL_PROGRAM_STATISTICS,
            },
          },
        },
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: AdminDocumentConstants.TOP,
          },
          borderRadius: 8,
          borderRadiusApplication: AdminDocumentConstants.AROUND,
          borderRadiusWhenStacked: AdminDocumentConstants.LAST,
          columnWidth: AdminDocumentConstants.SEVENTY_PERCENTAGE,
        },
      },
      title: {
        text: AdminDocumentConstants.GIFT_PROMOTIONS,
        style: {
          fontSize: AdminDocumentConstants.FONT_SIZE_20PX,
          fontWeight: AdminDocumentConstants.NORMAL,
        },
      },
      stroke: {
        curve: AdminDocumentConstants.SMOOTH,
      },
      tooltip: {
        followCursor: true,
        x: {
          show: true,
          format: AdminDocumentConstants.YYYY,
        },
      },
      dataLabels: {
        enabled: true,
        offsetY: -20,
        style: {
          fontSize: AdminDocumentConstants.FONT_SIZE_12PX,
          colors: ['#000'],
        },
        formatter: function (val: any) {
          return val + AdminDocumentConstants.PERCENTAGE;
        },
      },
      xaxis: {
        type: AdminDocumentConstants.DATETIME,
        labels: {
          datetimeFormatter: {
            year: AdminDocumentConstants.YYYY,
          },
        },
      },
    };
  }

  resetCharts() {
    setTimeout(() => {
      this.chart?.resetSeries();
      this.barChart?.resetSeries();
    }, 250);
  }
}
