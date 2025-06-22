import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CommonModule } from '@angular/common';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzInputModule } from 'ng-zorro-antd/input';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { catchError, of } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { EmploymentTypeService } from '../../service/employment-type.service';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { EmploymentType } from '../../interfaces/employmentType';

@Component({
  selector: 'app-employment-type',
  standalone: true,
  imports: [
    NzFlexModule,
    NzToolTipModule,
    NzEmptyModule,
    NzListModule,
    NzButtonModule,
    NzFormModule,
    CommonModule,
    NzModalModule,
    ReactiveFormsModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule
  ],
  templateUrl: './employment-type.component.html',
  styleUrls: ['./employment-type.component.css', '/src/styles.css'],
})
export class EmploymentTypeComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  employmentTypes: EmploymentType[] = [];
  constructor(
    public service: EmploymentTypeService,
    private message: NzMessageService,
    private modal: NzModalService
  ) { }

  ngOnInit() {
    this.getData();
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getData() {
    this.service
      .getEmploymentTypes()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.employmentTypes = res;
      });
  }

  showModal() {
    this.isModelVisible = true;
  }

  edit(item: any) {
    this.service.populateEditForm(item);
    this.isEditModalVisible = true;
  }

  delete(id: any) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.EMPLOYMENT_TYPE_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteEmploymentType(id).subscribe((res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.EMPLOYMENT_TYPE_DELETED_MSG);
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createEmploymentTypeModel.invalid) {
      Object.values(this.service.createEmploymentTypeModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createEmploymentType().subscribe({
        next: (res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.EMPLOYMENT_TYPE_CREATED_MSG);
          this.service.createEmploymentTypeModel.reset();
          this.isModelVisible = false;
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.EMPLOYMENT_TYPE_EXISTS_MSG);
          }
          else {
            this.message.error(AdminDocumentConstants.ERROR_EMPLOYMENT_TYPE_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createEmploymentTypeModel.reset();
          }
        },
      });
    }
  }

  updateEmploymentType(): void {
    if (this.service.editEmploymentTypeModel.valid) {
      this.service.updateEmploymentType().subscribe({
        next: (res: any) => {
          this.message.success(AdminDocumentConstants.EMPLOYMENT_TYPE_UPDATED_MSG);
          this.getData();
          this.service.createEmploymentTypeModel.reset();
          this.isEditModalVisible = false;
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.EMPLOYMENT_TYPE_EXISTS_MSG);
          }
          else {
            this.message.error(AdminDocumentConstants.ERROR_EMPLOYMENT_TYPE_UPDATED_MSG);
            this.isModelVisible = false;
            this.service.createEmploymentTypeModel.reset();
          }
        },
      });
    }
    else {
      Object.values(this.service.editEmploymentTypeModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleCancel() {
    this.isModelVisible = false;
    this.isEditModalVisible = false;
  }
}
