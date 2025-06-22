import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Component, Input, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import {
  FormControl,
  FormGroup,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { CertificationsService } from '../../service/certifications.service';
import { Certification } from '../../../candidate/interfaces/Certification';
import { Skill } from '../../../candidate/interfaces/Skill';
import { SkillService } from '../../../candidate/services/skill.service';
import { ICertification } from '../../interfaces/ICertification';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { environment } from '../../../../../environments/environment';
import { concatMap } from 'rxjs';
import { MstCertificationService } from '../../../../shared/services/mst-certification.service';
import { ComponyService } from '../../service/compony.service';

@Component({
  selector: 'app-certifications',
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
    NzEmptyModule,
    NzTypographyModule
  ],
  templateUrl: './certifications.component.html',
  styleUrls: ['./certifications.component.css', '/src/styles.css'],
})
export class CertificationsComponent implements OnInit {
  certifications: ICertification[] = [];
  certificationsList: any[] = [];
  companies: any[] = [];
  skills: Skill[] = [];
  uploading = false;
  fileList: NzUploadFile[] = [];
  @Input() userId: string = '';
  imageUrl = environment.imageUrl;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private msg: NzMessageService,
    private certificationService: CertificationsService,
    private skillService: SkillService,
    private mstCertificationService: MstCertificationService,
    private companyService: ComponyService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formCertificate = this.fb.group({
      skillGuid: [''],
      userCertificationGuid: [''],
      certificationGuid: ['', [required]],
      companyGuid: ['', [required]],
      certificationIssueDate: [''],
      certificationExpirationDate: [''],
      certificationCredentialId: ['', [maxLength(250), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERICS)]],
      certificationCredentialUrl: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_BRACKETS)]],
      certificationPhotoName: ['', []],
      certificationDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  ngOnInit() {
    this.getCertifications();
  }

  isCertificationModalVisible: boolean = false;
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  fallback = AdminDocumentConstants.FALLBACK_IMAGE;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  initLoading = false;
  loadingMore = false;
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  startValue: Date | null = null;
  endValue: Date | null = null;

  formCertificate: FormGroup<{
    skillGuid: FormControl<string>;
    userCertificationGuid: FormControl<string>;
    certificationGuid: FormControl<string>;
    companyGuid: FormControl<string>;
    certificationIssueDate: FormControl<string>;
    certificationExpirationDate: FormControl<string>;
    certificationCredentialId: FormControl<string>;
    certificationCredentialUrl: FormControl<string>;
    certificationPhotoName: FormControl<string>;
    certificationDescription: FormControl<string>;
  }>;

  getCertifications() {
    this.certificationService.getCertificates(this.userId).pipe(concatMap((response) => {
      this.certifications = response;

      return this.skillService.getSkills(this.userId);
    })).pipe(concatMap((response) => {
      this.skills = response;

      return this.companyService.getComponies(0, 0);
    })).pipe(concatMap((response: any) => {
      this.companies = response.list;

      return this.mstCertificationService.getAllCertifications();
    })).subscribe(
      {
        next: (response: any) => {
          this.certificationsList = response.list;
        },
        error: (err) => {
          console.log(err);
        }
      }
    );
  }

  beforeUpload = (file: NzUploadFile): boolean => {
    const isJpgOrPng = file.type === sharedConstant.IMAGE_JPEG || file.type === sharedConstant.IMAGE_PNG;
    if (!isJpgOrPng) {
      this.msg.error(sharedConstant.IMAGE_VALIDATION_MESSAGE);
      return false;
    } else {
      this.fileList = this.fileList.concat(file);
      this.fileList = this.fileList.slice(-1);
      this.fileList = this.fileList;
      return false;
    }
  };

  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue || !this.endValue) {
      return false;
    }
    return startValue.getTime() > this.endValue.getTime();
  };

  disabledEndDate = (endValue: Date): boolean => {
    if (!endValue || !this.startValue) {
      return false;
    }
    return endValue.getTime() <= this.startValue.getTime();
  };

  handleStartOpenChange(): void {
    if (this.formCertificate.controls.certificationIssueDate.value && this.formCertificate.controls.certificationIssueDate.value !== '') {
      this.startValue = new Date(this.formCertificate.controls.certificationIssueDate.value);
    }
    else
      this.startValue = null;
  }

  handleEndOpenChange(): void {
    if (this.formCertificate.controls.certificationExpirationDate.value && this.formCertificate.controls.certificationExpirationDate.value !== '') {
      this.endValue = new Date(this.formCertificate.controls.certificationExpirationDate.value);
    }
    else
      this.endValue = null;
  }

  handleCertificateModalOk(): void {
    if (this.formCertificate.valid) {
      if (this.formCertificate.value.userCertificationGuid === '') {
        this.certificationService
          .createCertificate(
            this.userId,
            this.formCertificate.value,
            this.fileList
          )
          .subscribe({
            next:
              (response: ICertification) => {
                this.getCertifications();
                this.formCertificate.reset();
                this.fileList = [];
                this.isCertificationModalVisible = false;
                this.msg.success(sharedConstant.CERTIFICATE_ADD)
                this.startValue = null;
                this.endValue = null;
              },
            error: () => this.msg.error(sharedConstant.CERTIFICATE_ERROR_ADD)
          });
      } else {
        this.certificationService
          .editCertificate(
            this.userId,
            this.formCertificate.value,
            this.fileList
          )
          .subscribe({
            next:
              (response: ICertification) => {
                this.getCertifications();
                this.formCertificate.reset();
                this.fileList = [];
                this.isCertificationModalVisible = false;
                this.msg.success(sharedConstant.CERTIFICATE_UPDATE)
                this.startValue = null;
                this.endValue = null;
              },
            error: () => this.msg.error(sharedConstant.CERTIFICATE_ERROR_UPDATE)
          });
      }
    } else {
      Object.values(this.formCertificate.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleCertificateModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formCertificate.reset();
    this.fileList = [];
    this.isCertificationModalVisible = false;
  }

  showCertificateModal(certification?: Certification): void {
    if (certification) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formCertificate.patchValue(certification);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isCertificationModalVisible = true;
  }

  showCertificationDeleteConfirm(certificationId: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.CERTIFICATIONS_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.certificationService
          .deleteCertificate(this.userId, certificationId)
          .subscribe({
            next:
              () => {
                (this.certifications = this.certifications.filter(
                  (certification) =>
                    certification.userCertificationGuid !== certificationId))
                this.msg.success(sharedConstant.CERTIFICATE_DELETE)
              },
            error: () => this.msg.error(sharedConstant.CERTIFICATE_ERROR_DELETE)
          }),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  showCertificateImageDeleteConfirm(): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.CERTIFICATIONS_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.formCertificate.value.certificationPhotoName = undefined;
        this.fileList = [];
        this.certificationService.editCertificate(
          this.userId, { ...this.formCertificate.value, certificationPhotoName: null }, this.fileList).subscribe({
            next:
              (response: Certification) => {
                let index = this.certifications.findIndex(
                  (certification) =>
                    this.formCertificate.value.userCertificationGuid ==
                    certification.userCertificationGuid
                );

                this.certifications[index] = {
                  ...this.certifications[index],
                  ...response,
                };

                this.formCertificate.reset();
                this.fileList = [];
                this.isCertificationModalVisible = false;

                this.msg.success(sharedConstant.CERTIFICATE_UPDATE);
              },
            error: (err: any) => {
              this.msg.error(sharedConstant.CERTIFICATE_ERROR_UPDATE);
            }
          })
      },
      nzOnCancel: () => { }
    })
  }
}
