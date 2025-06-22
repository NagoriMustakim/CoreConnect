import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { CommonModule } from '@angular/common';
import { NzCardModule } from 'ng-zorro-antd/card';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { GiftProgramService } from '../../services/gift-program.service';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { Status } from '../../../../shared/enums/status.enum';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { IGiftProgram } from '../../interfaces/IGiftProgram';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';

@Component({
  selector: 'app-gift-program',
  standalone: true,
  imports: [
    NzIconModule,
    NzFormModule,
    CommonModule,
    NzCardModule,
    NzUploadModule,
    NzModalModule,
    NzInputModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzSelectModule,
    NzCheckboxModule,
    NzListModule,
    NzTabsModule,
    NzTimelineModule,
    NzSpaceModule,
    NzAvatarModule,
    RouterOutlet,
    RouterLink,
    NzTypographyModule,
    NzFlexModule,
    NzDatePickerModule,
    NzSkeletonModule,
    NzTagModule,
    NzEmptyModule,
    NzTableModule,
    NzPaginationModule,
  ],
  templateUrl: './gift-program.component.html',
  styleUrls: ['./gift-program.component.css', '/src/styles.css'],
})

export class GiftProgramComponent implements OnInit {
  statusList = [
    { text: Status[Status.Pending], value: 0, checked: true },
    { text: Status[Status.Approved], value: 1, checked: false },
    { text: Status[Status.Rejected], value: 2, checked: false }
  ];

  giftStatusForm: FormGroup<{
    giftformGuid: FormControl<string>;
    giftformAdminStatus: FormControl<number>;
    giftformManagerStatus: FormControl<number>;
    giftformRejectionReason: FormControl<string>;
  }>;

  previewImage: string | undefined = '';
  previewVisible = false;
  isGiftModalVisible = false;
  isGiftRejectionModalVisible = false;
  initLoading = true;
  details: any;
  loadingMore = false;
  applicationrequests: any[] = [];
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  currentPage = 1;
  pageSize = 10;
  total = 0;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  constructor(
    private fb: NonNullableFormBuilder,
    private giftService: GiftProgramService,
    private eventEmitterService: EventEmitterService

  ) {
    const { required } = CustomValidators;
    this.giftStatusForm = this.fb.group({
      giftformGuid: [''],
      giftformAdminStatus: [0, required],
      giftformManagerStatus: [0],
      giftformRejectionReason: [''],
    });
  }

  ngOnInit() {
    this.loadGiftDetails([]);
  }

  loadGiftDetails(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
    this.giftService.getAllGiftDetails(filter, pageNumber, pageSize).subscribe((response: any) => {
      this.applicationrequests = response.list;
      this.total = response.totalCount;
      this.eventEmitterService.callGetDetails();
      this.initLoading = false;
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, filter } = params;
    this.loadGiftDetails(filter, pageIndex, pageSize);
  }

  getStatus(status: Status): string {
    return Status[status];
  }

  showGiftForm(application: IGiftProgram): void {
    this.details = application;
    this.isGiftModalVisible = true;
  }

  handleApprove(e: MouseEvent, userId: any, giftProgramId: any): void {
    e.preventDefault();
    this.giftStatusForm.value.giftformManagerStatus = 1;
    this.giftService.putGiftFormStatus(this.giftStatusForm.value, userId, giftProgramId).subscribe((response: any) => {

      let index = this.applicationrequests.findIndex((application) => giftProgramId == application.giftformGuid);

      this.applicationrequests[index] = { ...this.applicationrequests[index], ...this.giftStatusForm.value };

      this.isGiftModalVisible = false;
    });
  }

  handleReason(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftRejectionModalVisible = true;
  }

  handleReject(e: MouseEvent, userId: any, giftProgramId: any): void {
    e.preventDefault();
    this.giftStatusForm.value.giftformManagerStatus = 2;
    this.giftService.putGiftFormStatus(this.giftStatusForm.value, userId, giftProgramId).subscribe((response: any) => {
      let index = this.applicationrequests.findIndex((application) => giftProgramId == application.giftformGuid);

      this.applicationrequests[index] = { ...this.applicationrequests[index], ...this.giftStatusForm.value };
      this.isGiftRejectionModalVisible = false;
      this.isGiftModalVisible = false;
      this.giftStatusForm.reset();
    });
  }

  handleRejectModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftRejectionModalVisible = false;
  }

  handleGiftModalCancel(): void {
    this.isGiftModalVisible = false;
  }


}
