import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { ComponyService } from './../../service/compony.service';
import { Company } from '../../interfaces/company';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { Component, OnInit } from '@angular/core';
import {
  NonNullableFormBuilder,
  ReactiveFormsModule,
  FormGroup,
  FormControl,
} from '@angular/forms';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { sharedConstant } from '../../../../shared/constants/sharedDocumentConstant';

@Component({
  selector: 'app-company',
  standalone: true,
  imports: [
    NzFlexModule,
    NzIconModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzTableModule,
    CommonModule,
    ReactiveFormsModule,
    NzSpaceModule,
    NzEmptyModule,
    NzTypographyModule,
    NzModalModule,
    NzInputModule,
    NzTableModule,
    NzFormModule
  ],
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css', '/src/styles.css'],
})
export class CompanyComponent implements OnInit {
  companies: Company[] = [];
  total: number = 0;
  pageSize: number = 10;
  saveMsg: string = '';
  isComponyModalVisible = false;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  formCompony: FormGroup<{
    companyGuid: FormControl<string>;
    companyName: FormControl<string>;
    companyDescription: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private componyService: ComponyService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;

    this.formCompony = this.fb.group({
      companyGuid: [''],
      companyName: [
        '',
        [
          required,
          minLength(2),
          maxLength(100),
          pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS),
        ],
      ],
      companyDescription: [
        '',
        [
          maxLength(500),
          pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS),
        ],
      ],
    });
  }

  ngOnInit() {
    this.getComponies();
  }

  getComponies(pageNumber: number = 1, pageSize: number = 10) {
    this.componyService.getComponies(pageNumber, pageSize).subscribe({
      next: (res: any) => {
        this.companies = res.list;
        this.total = res.count;
      },
    });
  }
  showComponyModal(compony?: Company): void {
    if (compony) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formCompony.patchValue(compony);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isComponyModalVisible = true;
  }

  handleCompanyModalOk(): void {
    if (this.formCompony.valid) {
      if (this.formCompony.value.companyGuid === '') {
        this.componyService.addCompony(this.formCompony.value).subscribe({
          next: (response: Company) => {
            this.companies.unshift(response);
            this.isComponyModalVisible = false;
            this.getComponies();
            this.formCompony.reset();

            //TODO: Chnage the error messages

            this.message.success(sharedConstant.COMPANY_ADD);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.COMPANY_ERROR_ADD);
            this.formCompony.reset();
            this.isComponyModalVisible = false;
          },
        });
      } else {
        this.componyService.updateCompony(this.formCompony.value).subscribe({
          next: (response: Company) => {
            let index = this.companies.findIndex(
              (designation) =>
                this.formCompony.value.companyGuid == designation.companyGuid
            );

            this.companies[index] = {
              ...this.companies[index],
              ...response,
            };
            console.log(this.companies);

            this.formCompony.reset();
            this.isComponyModalVisible = false;
            this.message.success(sharedConstant.COMPANY_UPDATE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.COMPANY_ERROR_UPDATE);
          },
        });
      }
    } else {
      Object.values(this.formCompony.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleCompanyModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formCompony.reset();
    this.isComponyModalVisible = false;
  }

  showComponyDeleteConfirm(componyId: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.COMPANY_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.componyService.deleteCompony(componyId).subscribe({
          next: (response: any) => {
            this.companies = this.companies.filter(
              (compony) => compony.companyGuid !== componyId
            );
            this.getComponies();
            this.message.success(sharedConstant.COMPANY_DELETE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.COMPANY_ERROR_DELETE);
          },
        }),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => {},
    });
  }
}
