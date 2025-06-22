import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
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
import { NzTypographyModule } from 'ng-zorro-antd/typography';
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
import { CertificationService } from '../../services/certification.service';
import { Certification } from '../../interfaces/Certification';
import { Skill } from '../../interfaces/Skill';
import { SkillService } from '../../services/skill.service';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { environment } from '../../../../../environments/environment';
import { concatMap } from 'rxjs';
import { MstCertificationService } from '../../../../shared/services/mst-certification.service';
import { ComponyService } from '../../../admin/service/compony.service';

@Component({
  selector: 'app-certification',
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
  templateUrl: './certification.component.html',
  styleUrls: ['./certification.component.css', '/src/styles.css'],
})
export class CertificationComponent implements OnInit {
  @Input() userId: string = '';
  certifications: Certification[] = [];
  certificationsList: any[] = [];
  companies: any[] = [];
  skills: Skill[] = [];
  uploading = false;
  fileList: NzUploadFile[] = [];
  imageUrl = environment.imageUrl;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private certificationService: CertificationService,
    private skillService: SkillService,
    private mstCertificationService: MstCertificationService,
    private companyService: ComponyService
  ) {
    const { required, maxLength, pattern } = CustomValidators;
    this.formCertificate = this.fb.group({
      skillGuid: [''],
      userCertificationGuid: [''],
      certificationGuid: ['', [required]],
      companyGuid: ['', [required]],
      certificationIssueDate: [''],
      certificationExpirationDate: [''],
      certificationCredentialId: ['', [maxLength(250), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS)]],
      certificationCredentialUrl: ['', [maxLength(500), pattern(ValidationConstants.REGEX_PATTERN_NO_BRACKETS)]],
      certificationPhotoName: ['', []],
      certificationDescription: ['', [maxLength(500), pattern(ValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  ngOnInit() {
    this.certificationService.getCertifications(this.userId).pipe(concatMap((response) => {
      this.certifications = response;

      return this.skillService.getSkills(this.userId);
    })).pipe(concatMap((response) => {
      this.skills = response;

      return this.companyService.getComponies(0, 0);
    }))
      .pipe(concatMap((response: any) => {
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

  isCertificationModalVisible: boolean = false;
  saveMsg: string = DocumentConstants.ADD_MSG;
  fallback = DocumentConstants.FALLBACK_IMAGE;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
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

  beforeUpload = (file: NzUploadFile): boolean => {
    const isJpgOrPng = file.type === sharedConstant.IMAGE_JPEG || file.type === sharedConstant.IMAGE_PNG;
    if (!isJpgOrPng) {
      this.message.error(sharedConstant.IMAGE_VALIDATION_MESSAGE);
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
          .addCertification(this.formCertificate.value, this.fileList)
          .subscribe({
            next:
              (response: Certification) => {
                this.certifications.push(response);
                this.formCertificate.reset();
                this.fileList = [];
                this.isCertificationModalVisible = false;
                this.message.success(sharedConstant.CERTIFICATE_ADD);
                this.startValue = null;
                this.endValue = null;
              },
            error: (err: any) => {
              this.message.error(sharedConstant.CERTIFICATE_ERROR_ADD);
              this.formCertificate.reset();
              this.isCertificationModalVisible = false;
            }
          });
      } else {
        this.certificationService
          .updateCertification(this.formCertificate.value, this.fileList)
          .subscribe({
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

                this.message.success(sharedConstant.CERTIFICATE_UPDATE);
                this.startValue = null;
                this.endValue = null;
              },
            error: (err: any) => {
              this.message.error(sharedConstant.CERTIFICATE_ERROR_UPDATE);
            }
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
      this.saveMsg = DocumentConstants.EDIT_MSG;
      this.formCertificate.patchValue(certification);
      this.handleStartOpenChange();
      this.handleEndOpenChange();
      this.disabledStartDate(new Date(certification!.certificationIssueDate));
      this.disabledEndDate(new Date(certification!.certificationExpirationDate));
    } else {
      this.saveMsg = DocumentConstants.ADD_MSG;
    }

    this.isCertificationModalVisible = true;
  }

  showCertificationDeleteConfirm(certificationId: string): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.CERTIFICATIONS_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.certificationService
          .deleteCertification(certificationId)
          .subscribe({
            next: (response: any) => {
              (this.certifications = this.certifications.filter(
                (certification) =>
                  certification.userCertificationGuid !== certificationId
              ))
              this.message.success(sharedConstant.CERTIFICATE_DELETE)
            },
            error: (err: any) => {
              this.message.error(sharedConstant.CERTIFICATE_ERROR_DELETE)
            }
          }),
      nzCancelText: DocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  showCertificateImageDeleteConfirm(): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.CERTIFICATIONS_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.formCertificate.value.certificationPhotoName = undefined;
        this.fileList = [];
        this.certificationService.updateCertification({ ...this.formCertificate.value, certificationPhotoName: null }, this.fileList).subscribe({
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

              this.message.success(sharedConstant.CERTIFICATE_UPDATE);
            },
          error: (err: any) => {
            this.message.error(sharedConstant.CERTIFICATE_ERROR_UPDATE);
          }
        })
      },
      nzOnCancel: () => { }
    })
  }
}
