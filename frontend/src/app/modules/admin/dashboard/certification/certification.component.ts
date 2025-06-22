import { Component, OnInit } from '@angular/core';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzTableModule } from 'ng-zorro-antd/table';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { Certification } from '../../../candidate/interfaces/Certification';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { sharedConstant } from '../../../../shared/constants/sharedDocumentConstant';
import { CertificationService } from '../../service/certification.service';
import { NzTypographyComponent } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-certification',
  standalone: true,
  imports: [
    NzListModule,
    ReactiveFormsModule,
    NzIconModule,
    NzInputModule,
    NzButtonModule,
    NzEmptyModule,
    NzModalModule,
    NzTableModule,
    NzPaginationModule,
    NzFormModule,
    CommonModule,
    NzSelectModule,
    NzDatePickerModule,
    NzFlexModule,
    NzSpaceModule,
    NzTypographyComponent
  ],
  templateUrl: './certification.component.html',
  styleUrls: ['./certification.component.css', '/src/styles.css']
})
export class CertificationComponent implements OnInit {

  certifications: Certification[] = [];
  isCertificationModalVisible: boolean = false;
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  pageSize = 10;
  total = 0;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  formCertificate: FormGroup<{
    certificationGuid: FormControl<string>;
    certificationTitle: FormControl<string>;
    certificationDescription: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private certificationService: CertificationService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formCertificate = this.fb.group({
      certificationGuid: [''],
      certificationTitle: ['', [required, minLength(2), maxLength(100), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN)],],
      certificationDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  ngOnInit() {
    this.getCertifications();
  }

  getCertifications(pageNumber: number = 1, pageSize: number = 10) {
    this.certificationService.getCertifications(pageNumber, pageSize).subscribe((response: any) => {
      this.certifications = response.list;
      this.total = response.count;
    });
  }

  handleCertificateModalOk(): void {
    if (this.formCertificate.valid) {
      if (this.formCertificate.value.certificationGuid === '') {
        this.certificationService
          .addCertification(this.formCertificate.value)
          .subscribe({
            next:
              (response: Certification) => {
                this.getCertifications();
                this.formCertificate.reset();
                this.isCertificationModalVisible = false;
                this.message.success(sharedConstant.CERTIFICATE_ADD);
              },
            error: (err: any) => {
              this.message.error(sharedConstant.CERTIFICATE_ERROR_ADD);
              this.formCertificate.reset();
              this.isCertificationModalVisible = false;
            }
          });
      } else {
        this.certificationService
          .updateCertification(this.formCertificate.value)
          .subscribe({
            next:
              (response: Certification) => {
                this.getCertifications();
                this.formCertificate.reset();
                this.isCertificationModalVisible = false;
                this.message.success(sharedConstant.CERTIFICATE_UPDATE);
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
          .deleteCertification(certificationId)
          .subscribe({
            next: (response: any) => {
              this.getCertifications();
              this.message.success(sharedConstant.CERTIFICATE_DELETE)
            },
            error: (err: any) => {
              this.message.error(sharedConstant.CERTIFICATE_ERROR_DELETE)
            }
          }),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }
}
