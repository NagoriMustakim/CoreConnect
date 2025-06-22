import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import {
  NonNullableFormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { RequestService } from './../../service/request.service';
import { Request } from './../../interfaces/Request';
import { Component, OnInit } from '@angular/core';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { FoodRequest } from '../../../../shared/enums/food-request.enum';
import { Status } from '../../../../shared/enums/status.enum';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzTypographyComponent } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-request',
  standalone: true,
  imports: [
    CommonModule,
    NzModalModule,
    NzListModule,
    NzButtonModule,
    NzGridModule,
    NzIconModule,
    NzInputModule,
    ReactiveFormsModule,
    NzCardModule,
    CommonModule,
    NzFormModule,
    NzTableModule,
    NzPaginationModule,
    NzFlexModule,
    NzTagModule,
    NzSpaceModule,
    NzTypographyComponent
  ],
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css', '/src/styles.css'],
})
export class RequestComponent implements OnInit {
  requests: Request[] = [];
  id: string = '';
  pageIndex = 1;
  pageSize = 10;
  total = 0;
  isVisible = false;
  statusList = [
    { text: Status[Status.Pending], value: 0, checked: true },
    { text: Status[Status.Approved], value: 1, checked: false },
    { text: Status[Status.Rejected], value: 2, checked: false }
  ];
  expandSet = new Set<string>();

  constructor(private fb: NonNullableFormBuilder, private requestService: RequestService, private modal: NzModalService, private message: NzMessageService) {
    const { required, pattern, maxLength } = CustomValidators
    this.validateRejectForm = this.fb.group({
      rejectionReason: ['', [required, maxLength(100), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE)]],
    });
  }

  ngOnInit() {
    this.getAllRequests([], this.pageIndex, this.pageSize);
  }

  getAllRequests(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
    this.requestService.getAllRequests(filter, pageNumber, pageSize).subscribe((response: any) => {
      this.requests = response.list;
      this.total = response.totalCount;
    });
  }
  onExpandChange(requestGuid: string, checked: boolean): void {
    if (checked) {
      this.expandSet.add(requestGuid);
    } else {
      this.expandSet.delete(requestGuid);
    }
  }
  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, filter } = params;
    this.getAllRequests(filter, pageIndex, pageSize);
  }

  validateRejectForm: FormGroup<{
    rejectionReason: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ApproveRequest(requestGuid: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.REQUEST_APPROVAL_MSG,
      nzOnOk: () => {
        let value = {
          status: 1,
        };
        this.requestService.ApproveRequest(value, requestGuid).subscribe({
          next: (response: any) => {
            this.message.success(AdminDocumentConstants.REQUEST_APPROVED_MSG);
            this.getAllRequests([], this.pageIndex, this.pageSize);
          },
          error: (err: any) => console.log(err),
        });
      },
    });
  }

  RejectRequest(value: any, id: string) {
    value.status = 2;
    this.requestService.RejectRequest(value, id).subscribe({
      next: (res: any) => {
        this.message.success(AdminDocumentConstants.REQUEST_REJECTED_MSG);
        this.getAllRequests([], this.pageIndex, this.pageSize);
      },
      error: (err: any) => {
        this.message.error(AdminDocumentConstants.ERROR_TRY_AGAIN_MSG);
      },
    })
  }

  DeleteModal(id: string) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.REQUEST_DELETE_CONFIRMATION_MSG,
      nzOnOk: () => {
        this.DeleteRequest(id);
      },
    });
  }
  DeleteRequest(id: string) {
    this.requestService.DeleteRequest(id).subscribe({
      next: (res: any) => {
        this.message.success(AdminDocumentConstants.REQUEST_DELETED_MSG);
        this.getAllRequests([], this.pageIndex, this.pageSize);
      },
      error: (err: any) => {
        this.message.error(AdminDocumentConstants.ERROR_TRY_AGAIN_MSG);
      },
    });
  }

  showModal(id: string): void {
    console.log(id);

    this.id = id;
    this.isVisible = true;
  }
  createMessage(type: string, mes: string): void {
    this.message.create(type, mes);
  }

  handleRejectModalReject() {
    console.log(this.validateRejectForm.value);

    if (this.validateRejectForm.valid) {
      this.RejectRequest(this.validateRejectForm.value, this.id);
      this.isVisible = false;
      this.validateRejectForm.reset();
    } else {
      Object.values(this.validateRejectForm.controls).forEach(
        (control: any) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    }
  }

  handleRejectModalCancel(): void {
    this.isVisible = false;
    this.validateRejectForm.reset();
  }

  getStatus(status: Status): string {
    return Status[status];
  }

  getFoodStatus(foodStatus: FoodRequest): string {
    return FoodRequest[foodStatus];
  }


}
