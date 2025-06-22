import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { CommonModule } from '@angular/common';
import { NzListModule } from 'ng-zorro-antd/list';
import { INomination } from './../../interfaces/INomination';
import { NominationServiceService } from './../../service/nomination-service.service';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Status } from '../../../../shared/enums/status.enum';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { Nomination } from '../../../candidate/interfaces/Nomination';
import { NzImageModule, NzImageService } from 'ng-zorro-antd/image';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-nomination',
  standalone: true,
  imports: [
    NzListModule,
    CommonModule,
    ReactiveFormsModule,
    NzEmptyModule,
    NzButtonModule,
    NzIconModule,
    NzFlexModule,
    NzModalModule,
    NzTableModule,
    NzPaginationModule,
    NzDividerModule,
    NzTagModule,
    NzTypographyModule,
    NzPageHeaderModule,
    NzToolTipModule,
    NzFormModule,
    NzInputModule,
    NzImageModule
  ],
  templateUrl: './nomination.component.html',
  styleUrls: ['./nomination.component.css', '/src/styles.css'],
})
export class NominationComponent {
  programId: string = '';
  details : any;
  isReviewModalVisible = false;
  isNominationRejectionModalVisible = false;
  nominations: INomination[] = [];
  pageSize = 10;
  total = 0;
  statusList = [
    { text: Status[Status.Pending], value: 0, checked: true },
    { text: Status[Status.Approved], value: 1, checked: false },
    { text: Status[Status.Rejected], value: 2, checked: false }
  ];
  internalProgramName: string = '';

  nominationStatusForm: FormGroup<{
    nominationGuid: FormControl<string>;
    nominationRejectionReason: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };
  imageUrl = environment.imageUrl;

  constructor(
    private route: ActivatedRoute,
    private fb: NonNullableFormBuilder,
    private nominationService: NominationServiceService,
    private modal: NzModalService,
    private msg: NzMessageService,
    private router: Router,
    private nzImageService: NzImageService
  ) {
    let id = this.route.snapshot.paramMap.get(AdminDocumentConstants.ID);
    if (window !== undefined && id !== null) {
      this.programId = id;
    }
    const { required } = CustomValidators;
    this.nominationStatusForm = this.fb.group({
      nominationGuid: [''],
      nominationRejectionReason: ['', required],
    });

    const navigation = this.router.getCurrentNavigation();
    this.internalProgramName = navigation?.extras.state!['program'];
    this.getData([]);
  }

  getData(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
    this.nominationService
      .getAllNominationById(filter, this.programId, pageNumber, pageSize)
      .subscribe((response: any) => {
        this.nominations = response.list;
        this.total = response.count;
      });
  }

  showReviewModal(item: any): void{

    this.details = item;
    console.log(this.details);
    this.isReviewModalVisible = true;
  }

  showDeleteConfirm(nominationId: string) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.NOMINATION_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {

        this.nominationService.DeleteNomination(nominationId).subscribe((response: any) => {
          this.msg.success(AdminDocumentConstants.NOMINATION_DELETED_MSG)
          this.nominations = this.nominations.filter((ip) => ip.nominationGuid !== nominationId)
        })
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  ApproveNomination(nominationId: string) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.NOMINATION_APPROVE_CONFIRMATION_MSG,
      nzOnOk: () => {
        this.nominationService.ApproveNomination(nominationId).subscribe((response: any) => {
          this.getData([]);
        });
        this.isReviewModalVisible = false;
      }
    });
  }

  // RejectNomination(nominationId: string) {
  //   this.modal.confirm({
  //     nzTitle: AdminDocumentConstants.NOMINATION_REJECT_CONFIRMATION_MSG,
  //     nzOnOk: () => {
  //       this.nominationService.RejectNomination(nominationId).subscribe((response: any) => {
  //         this.getData([]);
  //       });
  //     }
  //   });
  // }

  handleReject(e: MouseEvent, nominationId:string, details:any): void{

    if(this.nominationStatusForm.valid){
      this.nominationService.RejectNomination(nominationId, this.nominationStatusForm.value).subscribe((response: any) => {
      this.getData([]);
      });
      this.isNominationRejectionModalVisible = false;
      this.isReviewModalVisible = false;
    }
  }

  handleRejectionReason(e: MouseEvent): void {
    e.preventDefault();
    this.isNominationRejectionModalVisible = true;
  }

  handleRejectModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isNominationRejectionModalVisible = false;
  }

  handleGiftModalCancel():void{
    this.isReviewModalVisible = false;
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, filter } = params;
    this.getData(filter, pageIndex, pageSize);
  }

  onBack(): void {
    this.router.navigate(['/dashboard', 'nominations']);
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

