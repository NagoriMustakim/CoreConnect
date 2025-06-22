import { NzSelectModule } from 'ng-zorro-antd/select';
import { CommonModule } from '@angular/common';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';
import { DesignationService } from './../../service/designation.service';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Component, OnInit } from '@angular/core';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { Designation } from '../../interfaces/designation';
import {
  NonNullableFormBuilder,
  FormGroup,
  FormControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { sharedConstant } from '../../../../shared/constants/sharedDocumentConstant';
import { DepartmentService } from '../../service/department.service';

@Component({
  selector: 'app-designation',
  standalone: true,
  imports: [
    NzEmptyModule,
    NzTableModule,
    NzModalModule,
    ReactiveFormsModule,
    NzFormModule,
    NzInputModule,
    NzFlexModule,
    NzButtonModule,
    NzIconModule,
    NzTypographyModule,
    NzSpaceModule,
    CommonModule,
    NzSelectModule
  ],
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css', '/src/styles.css'],
})
export class DesignationComponent implements OnInit {
  designations: Designation[] = [];
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  isDesignationModal: boolean = false;
  total: number = 0;
  pageSize: number = 10;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };
  departments:any[]=[];

  formDesignation: FormGroup<{
    designationGuid: FormControl<string>;
    designation: FormControl<string>;
    designationLevel:FormControl<string>;
    departmentGuid:FormControl<string>;
    designationDescription: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private designationService: DesignationService,
    private departmentService: DepartmentService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formDesignation = this.fb.group({
      designationGuid: [''],
      designation: [
        '',
        [
          required,
          minLength(2),
          maxLength(100),
          pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS),
        ],
      ],
      departmentGuid:['',required],
      designationLevel:['',required],
      designationDescription: [
        '',
        [
          maxLength(500),
          pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS),
        ],
      ],
    });
  }

  ngOnInit() {
    this.getDesignations();
  }

  getDesignations(pageNumber: number = 1, pageSize: number = 10) {
    this.designationService.getDesignations(pageNumber, pageSize).subscribe({
      next: (res: any) => {
        this.designations = res.list;
        this.total = res.count;
      },
    });
  }

  handleDesignationModalOk(): void {
    if (this.formDesignation.valid) {
      if (this.formDesignation.value.designationGuid === '') {
        this.designationService
          .addDesignation(this.formDesignation.value)
          .subscribe({
            next: (response: Designation) => {
              this.designations.unshift(response);
              this.isDesignationModal = false;
              this.getDesignations();
              this.formDesignation.reset();
              this.message.success(sharedConstant.DESIGNATION_ADD);
            },
            error: (err: any) => {
              this.message.error(sharedConstant.DESIGNATION_ERROR_ADD);
              this.formDesignation.reset();
              this.isDesignationModal = false;
            },
          });
      } else {
        this.designationService
          .updateDesignation(this.formDesignation.value)
          .subscribe({
            next: (response: Designation) => {
              let index = this.designations.findIndex(
                (designation) =>
                  this.formDesignation.value.designationGuid ==
                  designation.designationGuid
              );

              this.designations[index] = {
                ...this.designations[index],
                ...response,
              };

              this.formDesignation.reset();
              this.isDesignationModal = false;
              this.message.success(sharedConstant.DESIGNATION_UPDATE);
            },
            error: (err: any) => {
              this.message.error(sharedConstant.DESIGNATION_ERROR_UPDATE);
            },
          });
      }
    } else {
      Object.values(this.formDesignation.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleDesignationModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formDesignation.reset();
    this.isDesignationModal = false;
  }

  showDesignationModal(designation?: Designation): void {
    this.departmentService.getAllDepartment(0,0).subscribe(
      (res:any)=>{
      this.departments=res.list;
      })
    if (designation) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formDesignation.patchValue(designation);
      console.log(this.formDesignation.value);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isDesignationModal = true;
  }
  showProjectDeleteConfirm(designationId: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.DESIGNATION_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.designationService.deleteDesignation(designationId).subscribe({
          next: (response: any) => {
            this.designations = this.designations.filter(
              (designations) => designations.designationGuid !== designationId
            );
            this.getDesignations();
            this.message.success(sharedConstant.DESIGNATION_DELETE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.DESIGNATION_ERROR_DELETE);
          },
        }),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => {},
    });
  }
}
