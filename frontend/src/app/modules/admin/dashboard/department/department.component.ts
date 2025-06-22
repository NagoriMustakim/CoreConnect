import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzMessageService } from 'ng-zorro-antd/message';
import { DesignationService } from '../../service/designation.service';
import { ValidationConstants } from '../../../candidate/constants/ValidationConstants';
import { DepartmentService } from '../../service/department.service';

@Component({
  selector: 'app-department',
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
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css','/src/styles.css']
})
export class DepartmentComponent {

  pageSize = 10;
  total = 0;
  // internalPrograms: any[] = [];
  departments: any[] = [];
  isDepartmentModalVisible: boolean = false;
  modalTitle : string = AdminDocumentConstants.ADD_MSG;

  formdepartment: FormGroup<{
    departmentGuid: FormControl<string>;
    department: FormControl<string>;
    departmentDescription: FormControl<string>;
  }>;

  constructor(
    private departmentService: DepartmentService,
    private designationServices: DesignationService,
    private modal: NzModalService,
    private msg: NzMessageService,
    private fb: NonNullableFormBuilder)
    {
      const { pattern, required, maxLength } = CustomValidators
      this.formdepartment = this.fb.group({
        departmentGuid:[''],
        department: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN)]],
        departmentDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      });
   }


  showDepartmentModal(Department?: any): void {
    // this.designationServices.getInternalPrograms().subscribe((response: any) => {
    //   this.internalPrograms = response.result;
    // });
    if (Department) {
      this.modalTitle = AdminDocumentConstants.EDIT_MSG;
      this.formdepartment.patchValue(Department);
    } else {
      this.modalTitle = AdminDocumentConstants.ADD_MSG;
    }
    this.isDepartmentModalVisible = true;
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getAllDepartments(pageNumber: number = 1, pageSize: number = 10){
    this.departmentService.getAllDepartment(pageNumber, pageSize)
    .subscribe((response: any) =>{
      this.departments = response.list;
      this.total = response.count;
    });
    console.log(this.departments);

  }

  handleDepartmentModalOk(): void {
    if (this.formdepartment.valid) {
      if (this.formdepartment.value.departmentGuid === '') {
        this.departmentService.addDepartment(this.formdepartment.value).subscribe({
          next:
          (response: any) => {
            this.departments.unshift(response);
            this.isDepartmentModalVisible = false;
            this.getAllDepartments();
            this.msg.success(AdminDocumentConstants.DEPARTMENT_CREATED_MSG);
            this.formdepartment.reset();
          },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_DEPARTMENT_CREATED_MSG);
            this.formdepartment.reset();
            this.isDepartmentModalVisible = false;
          }
        });
      }
      else{
        this.departmentService
        .updateDepartment(this.formdepartment.value)
        .subscribe({
          next:
            (response: any) => {
           this.getAllDepartments()
              let index = this.departments.findIndex(
                (department) =>
                  this.formdepartment.value.departmentGuid ==
                department.departmentGuid
              );

              this.departments[index] = {
                ...this.departments[index],
                ...response,
              };

              this.formdepartment.reset();
              this.isDepartmentModalVisible = false;

              this.msg.success(AdminDocumentConstants.DEPARTMENT_UPDATED_MSG);
            },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_DEPARTMENT_UPDATED_MSG);
          }
        });
        }
    }
    else {
      Object.values(this.formdepartment.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleDepartmentModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formdepartment.reset();
    this.isDepartmentModalVisible = false;
  }

  showDepartmentDeleteConfirm(departmentId: string){
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.DEPARTMENT_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {

        this.departmentService.deleteDepartment(departmentId).subscribe((response: any) => {
          this.msg.success(AdminDocumentConstants.DEPARMTENT_DELETED_MSG)
          this.getAllDepartments();
        })
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.getAllDepartments(pageIndex, pageSize);
  }


}
