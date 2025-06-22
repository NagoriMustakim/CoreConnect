import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzFormModule } from 'ng-zorro-antd/form';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { BusinessUnitService } from '../../service/business-unit.service';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError, of, Observable, Observer } from 'rxjs';
import { CommonModule } from '@angular/common';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzUploadFile, NzUploadModule, NzUploadChangeParam } from 'ng-zorro-antd/upload';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { environment } from '../../../../../environments/environment';


@Component({
  selector: 'app-business-units',
  standalone: true,
  imports: [
    NzListModule,
    NzFlexModule,
    NzSkeletonModule,
    CommonModule,
    ReactiveFormsModule,
    NzFormModule,
    NzButtonModule,
    NzIconModule,
    NzModalModule,
    NzInputModule,
    FormsModule,
    NzUploadModule,
    NzTypographyModule
  ],
  templateUrl: './business-units.component.html',
  styleUrls: ['./business-units.component.css', '/src/styles.css'],
})
export class BusinessUnitsComponent implements OnInit {
  editItem: Array<{
    loading: boolean;
    businessUnitName: any;
    businessUnitLogoName: string;
    businessUnitDescription: string;
    businessUnitGuid: string;
  }> = [];
  isVisible = false;
  isModelVisible = false;
  isConfirmLoading = false;
  value?: string;
  initLoading = true;
  loadingMore = false;
  data: any[] = [];
  list: Array<{
    loading: boolean;
    businessUnitName: any;
    businessUnitDescription: string;
    businessUnitGuid: string;
    businessUnitLogoName: string;
  }> = [];

  fileList: NzUploadFile[] = [];
  previewImage: string | undefined = '';
  previewVisible = false;
  formData: FormData | undefined;
  imageUrl=environment.imageUrl;

  constructor(
    private fb: FormBuilder,
    public service: BusinessUnitService,
    private message: NzMessageService,
    private msg: NzMessageService,
    private modal: NzModalService
  ) { }

  ngOnInit(): void {
    this.formData = new FormData();

    this.getData((res: any) => {
      this.data = res;
      this.list = res;

      this.initLoading = false;
    });
  }

  onSubmit(): void {
    if (this.service.createBusinessUnitForm.invalid) {
      Object.values(this.service.createBusinessUnitForm.controls).forEach(
        (control: any) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createBusinessUnit(this.service.createBusinessUnitForm.value, this.fileList).subscribe({
        next: (response: any) => {
          this.getData((res: any) => {
            this.data = res;
            this.list = res;
            this.initLoading = false;
          });

          this.message.success(AdminDocumentConstants.BUSINESS_UNIT_CREATED_MSG);
          this.fileList = [];
          this.service.createBusinessUnitForm.reset();
          this.isModelVisible = false;
        },
        error: (error: any) => {
          if (error.status === 409) {
            this.message.error(AdminDocumentConstants.BUSINESS_UNIT_ALREADY_EXISTS);
          } else {
            this.service.createBusinessUnitForm.reset();
            this.isModelVisible = false;
            this.message.error(AdminDocumentConstants.ERROR_BUSINESS_UNIT_CREATED_MSG);
          }
        },
      });
    }
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
      this.service.createBusinessUnitForm.controls['businessUnitLogoName'].setValue(file ? file.name : '');
      return false;
    }
  }
  showModal(): void {
    this.isModelVisible = true;
  }

  getData(callback: (res: any) => void): void {
    this.service
      .getBusinessUnits()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => callback(res));
  }

  edit(itemIn: any): void {
    this.isVisible = true;
    this.service.populateEditForm(itemIn);
  }

  delete(id: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.BUSINESS_UNIT_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteBusinessUnit(id).subscribe({
          next:
            (res: any) => {
              this.list = this.list.filter((item) => item.businessUnitGuid !== id);
              this.message.success(AdminDocumentConstants.BUSINESS_UNIT_DELETED_MSG);
            },
          error: (error: any) => {
            this.message.error(AdminDocumentConstants.SOMETHING_WENT_WRONG);
          }
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  handleOk(): void {
    this.isConfirmLoading = true;
    setTimeout(() => {
      this.isVisible = false;
      this.isConfirmLoading = false;
    }, 1000);
  }

  handelUpdate(): void {
    console.log(this.service.editBusinessUnitForm.valid);
    if (this.service.editBusinessUnitForm.valid) {
      this.isConfirmLoading = true;

      this.service.updateBusinessUnit(this.service.editBusinessUnitForm.value, this.fileList).subscribe({
        next:
          (response: any) => {
            this.getData((res: any) => {
              this.data = res;
              this.list = res;
              this.initLoading = false;
            });
            this.service.editBusinessUnitForm.reset();
            this.fileList = [];
            this.msg.success(AdminDocumentConstants.BUSINESS_UNIT_UPDATED_MSG);
          },
        error: (error: any) => {
          if (error.status === 409) {
            this.message.error(AdminDocumentConstants.BUSINESS_UNIT_ALREADY_EXISTS);
          } else {
            this.message.error(AdminDocumentConstants.SOMETHING_WENT_WRONG);
          }
        }
      });

      this.isConfirmLoading = false;
      this.isVisible = false;
    } else {
      Object.values(this.service.editBusinessUnitForm.controls).forEach(
        (control: any) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    }
  }

  handleCancel(): void {
    this.isVisible = false;
    this.isModelVisible = false;
    this.service.editBusinessUnitForm.reset();
    this.fileList = [];
  }
}
