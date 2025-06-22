import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { InternalProgramCategoriesService } from '../../service/internal-program-categories.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { ValidationConstants } from '../../../candidate/constants/ValidationConstants';
import { InternalProgramsService } from '../../service/internal-programs.service';

@Component({
  selector: 'app-internal-programs-categories',
  standalone: true,
  imports: [
    NzListModule,
    CommonModule,
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
    NzCardModule,
    NzTabsModule,
    NzCommentModule,
    NzFormModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzSpaceModule,
    NzImageModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzUploadModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzInputNumberModule,
    NzGridModule,
  ],
  templateUrl: './internal-programs-categories.component.html',
  styleUrls: ['./internal-programs-categories.component.css','/src/styles.css'],
})

export class InternalProgramsCategoriesComponent {

  pageSize = 10;
  total = 0;
  internalPrograms: any[] = [];
  internalProgramCategories: any[] = [];
  isIntenalProgramCategoryModalVisible: boolean = false;
  modalTitle : string = AdminDocumentConstants.ADD_MSG;

  formIntenalProgramCategory: FormGroup<{
    internalProgramGuid: FormControl<string>;
    internalProgramCategoryGuid: FormControl<string>;
    internalProgramCategory: FormControl<string>;
    internalProgramCategoryDescription: FormControl<string>;
  }>;

  constructor(
    private internalProgramCategoriesService: InternalProgramCategoriesService,
    private internalProgramServices: InternalProgramsService,
    private modal: NzModalService,
    private msg: NzMessageService,
    private fb: NonNullableFormBuilder)
    {
      const { pattern, required, maxLength } = CustomValidators
      this.formIntenalProgramCategory = this.fb.group({
        internalProgramGuid:['',[required]],
        internalProgramCategoryGuid: [''],
        internalProgramCategory: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN)]],
        internalProgramCategoryDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      });
   }


  showInternalProgramCategoryModal(internalprogramcategory?: any): void {
    this.internalProgramServices.getInternalPrograms().subscribe((response: any) => {
      this.internalPrograms = response.result;
    });
    if (internalprogramcategory) {
      this.modalTitle = AdminDocumentConstants.EDIT_MSG;
      this.formIntenalProgramCategory.patchValue(internalprogramcategory);
    } else {
      this.modalTitle = AdminDocumentConstants.ADD_MSG;
    }
    this.isIntenalProgramCategoryModalVisible = true;
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getAllInternalProgramCategories(pageNumber: number = 1, pageSize: number = 10){
    this.internalProgramCategoriesService.GetAllInternalProgramCategory(pageNumber, pageSize)
    .subscribe((response: any) =>{
      this.internalProgramCategories = response.list;
      this.total = response.count;
    });
  }

  handleInternalProgramCategoryModalOk(): void {
    if (this.formIntenalProgramCategory.valid) {
      if (this.formIntenalProgramCategory.value.internalProgramCategoryGuid === '') {
        this.internalProgramCategoriesService.AddInternalProgramCategory(this.formIntenalProgramCategory.value).subscribe({
          next:
          (response: any) => {
            this.internalProgramCategories.unshift(response);
            this.isIntenalProgramCategoryModalVisible = false;
            this.getAllInternalProgramCategories();
            this.msg.success(AdminDocumentConstants.INTERNAL_PROGRAM_CATEGORY_CREATED_MSG);
            this.formIntenalProgramCategory.reset();
          },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_INTERNAL_PROGRAM_CATEGORY_CREATED_MSG);
            this.formIntenalProgramCategory.reset();
            this.isIntenalProgramCategoryModalVisible = false;
          }
        });
      }
      else{
        this.internalProgramCategoriesService
        .UpdateInternalProgramCategory(this.formIntenalProgramCategory.value)
        .subscribe({
          next:
            (response: any) => {
           this.getAllInternalProgramCategories()
              let index = this.internalProgramCategories.findIndex(
                (internalprogramcategory) =>
                  this.formIntenalProgramCategory.value.internalProgramCategoryGuid ==
                internalprogramcategory.InternalProgramCategoryGuid
              );

              this.internalProgramCategories[index] = {
                ...this.internalProgramCategories[index],
                ...response,
              };

              this.formIntenalProgramCategory.reset();
              this.isIntenalProgramCategoryModalVisible = false;

              this.msg.success(AdminDocumentConstants.INTERNAL_PROGRAM_CATEGORY_UPDATED_MSG);
            },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_INTERNAL_PROGRAM_CATEGORY_UPDATED_MSG);
          }
        });
        }
    }
    else {
      Object.values(this.formIntenalProgramCategory.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleInternalProgramCategoryModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formIntenalProgramCategory.reset();
    this.isIntenalProgramCategoryModalVisible = false;
  }

  showInternalProgramCategoryDeleteConfirm(internalProgramCategoryId: string){
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.INTERNAL_PROGRAM_CATEGORY_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {

        this.internalProgramCategoriesService.DeleteInternalProgramCategory(internalProgramCategoryId).subscribe((response: any) => {
          this.msg.success(AdminDocumentConstants.INTERNAL_PROGRAM_CATEGORY_DELETED_MSG)
          this.getAllInternalProgramCategories();
        })
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.getAllInternalProgramCategories(pageIndex, pageSize);
  }

}
