import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCardModule } from 'ng-zorro-antd/card';
import { CommonModule } from '@angular/common';
import { INomination } from './../../../admin/interfaces/INomination';
import { NoiminationService } from './../../services/noimination.service';
import { Component, OnInit } from '@angular/core';
import { Status } from '../../../../shared/enums/status.enum';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { environment } from '../../../../../environments/environment';
import { NzImageModule, NzImageService } from 'ng-zorro-antd/image';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFlexModule } from 'ng-zorro-antd/flex';
@Component({
  selector: 'app-nominations',
  standalone: true,
  imports: [
    CommonModule,
    NzCardModule,
    NzGridModule,
    NzTypographyModule,
    NzEmptyModule,
    NzTableModule,
    NzTagModule,
    NzToolTipModule,
    NzImageModule,
    NzButtonModule,
    NzFlexModule
  ],
  templateUrl: './nominations.component.html',
  styleUrls: ['./nominations.component.css', '/src/styles.css'],
})
export class NominationsComponent implements OnInit {
  nomination: INomination[] = [];
  total = 0;
  pageSize: number = 10;
  statusList = [
    { text: Status[Status.Pending], value: 0, checked: true },
    { text: Status[Status.Approved], value: 1, checked: false },
    { text: Status[Status.Rejected], value: 2, checked: false }
  ];

  imageUrl = environment.imageUrl;
  constructor(private nominationService: NoiminationService,
    private eventEmitterService: EventEmitterService,
    private nzImageService: NzImageService
  ) { }

  ngOnInit() {
    this.getData([]);
  }

  getData(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
    this.nominationService.getNominations(filter, pageNumber, pageSize).subscribe((response: any) => {
      this.nomination = response.list;
      this.total = response.count;
      this.eventEmitterService.callGetDetails();
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, filter } = params;
    this.getData(filter, pageIndex, pageSize);
  }

  getStatus(status: Status): string {
    return Status[status];
  }

  OpenViewModal(nmsAttachments: any): void {
    let images: any = [];

    nmsAttachments.forEach((index: any) => {
      images.push({
        src: `${environment.imageUrl}/${index.attachmentName}`,
        alt: 'Loading...'
      })
    });
    console.log(images);

    this.nzImageService.preview(images, { nzZoom: 1.5, nzRotate: 0 });
  }

}