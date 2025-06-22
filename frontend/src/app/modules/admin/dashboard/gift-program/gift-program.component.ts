import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
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
import { GiftProgramService } from '../../service/gift-program.service';
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
import { NzMessageService } from 'ng-zorro-antd/message';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';

const getBase64 = (file: File): Promise<string | ArrayBuffer | null> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

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
    NzTableModule,
    NzPaginationModule,
    NzFlexModule,
    NzDatePickerModule,
    NzSkeletonModule,
    NzTagModule,
  ],
  templateUrl: './gift-program.component.html',
  styleUrls: ['./gift-program.component.css', '/src/styles.css'],
})

export class GiftProgramComponent implements OnInit {
  currentPage = 1;
  pageSize = 10;
  total = 0;
  loading = true;
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
  giftApplications: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  constructor(
    private fb: NonNullableFormBuilder,
    private giftService: GiftProgramService,
    private msg: NzMessageService,
  ) {
    const { required } = CustomValidators;
    this.giftStatusForm = this.fb.group({
      giftformGuid: [''],
      giftformAdminStatus: [0, required],
      giftformManagerStatus: [0],
      giftformRejectionReason: ['', required],
    });
  }

  ngOnInit() {
    this.loadGiftDetails([]);
  }

  loadGiftDetails(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
    this.loading = true;
    this.giftService.getAllGiftDetails(filter, pageNumber, pageSize).subscribe((response: any) => {
      this.loading = false;
      this.total = response.totalCount;
      this.giftApplications = response.list;

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

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }

  showGiftForm(application: IGiftProgram): void {
    console.log(application);
    this.details = application;
    this.isGiftModalVisible = true;
  }

  handleApprove(e: MouseEvent, userId: any, giftProgramId: any, details: any): void {
    if (details.giftformManagerStatus == 0) {
      this.msg.warning(AdminDocumentConstants.MANAGER_APPROVAL_PENDING_MSG)
    } else {
      e.preventDefault();
      this.giftStatusForm.value.giftformAdminStatus = 1;
      this.giftStatusForm.value.giftformManagerStatus = details.giftformManagerStatus;
      this.giftService.putGiftFormStatus(this.giftStatusForm.value, userId, giftProgramId, details.giftformReviewerId).subscribe((response: any) => {

        let index = this.giftApplications.findIndex((application) => giftProgramId == application.giftformGuid);

        this.giftApplications[index] = { ...this.giftApplications[index], ...this.giftStatusForm.value };

        this.isGiftModalVisible = false;
      });
    }
  }

  handleReason(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftRejectionModalVisible = true;
  }

  handleReject(e: MouseEvent, userId: any, giftProgramId: any, details: any): void {
    if (this.giftStatusForm.valid) {
      if (details.giftformManagerStatus == 0) {
        this.msg.warning(AdminDocumentConstants.MANAGER_APPROVAL_PENDING_MSG)
      } else {
        e.preventDefault();
        this.giftStatusForm.value.giftformAdminStatus = 2;
        this.giftStatusForm.value.giftformManagerStatus = details.giftformManagerStatus;
        this.giftService.putGiftFormStatus(this.giftStatusForm.value, userId, giftProgramId, details.giftformReviewerId).subscribe((response: any) => {
          let index = this.giftApplications.findIndex((application) => giftProgramId == application.giftformGuid);

          this.giftApplications[index] = { ...this.giftApplications[index], ...this.giftStatusForm.value };
          this.isGiftRejectionModalVisible = false;
          this.isGiftModalVisible = false;
          this.giftStatusForm.reset();
        });
      }
    } else {
      Object.values(this.giftStatusForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleRejectModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftRejectionModalVisible = false;
  }

  handleGiftModalCancel(): void {
    this.isGiftModalVisible = false;
  }
}
