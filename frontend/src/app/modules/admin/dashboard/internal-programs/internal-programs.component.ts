import { IInternalProgram } from './../../interfaces/IInternalProgram';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
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
import { InternalProgramsService } from '../../service/internal-programs.service';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzBadgeModule } from 'ng-zorro-antd/badge';

@Component({
  selector: 'app-internal-programs',
  standalone: true,
  imports: [
    NzFlexModule,
    NzEmptyModule,
    NzListModule,
    NzButtonModule,
    NzFormModule,
    CommonModule,
    NzModalModule,
    ReactiveFormsModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule,
    NzInputNumberModule,
    NzTagModule,
    NzCheckboxModule,
    NzBadgeModule
  ],
  templateUrl: './internal-programs.component.html',
  styleUrls: ['./internal-programs.component.css', '/src/styles.css'],
})
export class InternalProgramsComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  internalPrograms: IInternalProgram[] = [];
  constructor(
    public service: InternalProgramsService,
    private message: NzMessageService,
    private modal: NzModalService
  ) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.service
      .getInternalPrograms()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.internalPrograms = res.result;
      });
  }

  showModal() {
    this.isModelVisible = true;
  }
  edit(item: any) {
    this.service.populateEditForm(item);
    this.isEditModalVisible = true;
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  delete(id: any) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.INTERNAL_PROGRAM_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteInternalPrograms(id).subscribe((res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.INTERNAL_PROGRAM_DELETED_MSG);
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createInternalProgramsModel.invalid) {
      Object.values(this.service.createInternalProgramsModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createInternalProgram().subscribe({
        next: (res) => {
          this.getData();
          this.message.success(AdminDocumentConstants.INTERNAL_PROGRAM_CREATED_MSG);
          this.service.createInternalProgramsModel.reset();
          this.isModelVisible = false;
        },
        error: (err) => {
          if (err.status == 409) {
            this.message.error(AdminDocumentConstants.INTERNAL_PROGRAM_TYPE_EXISTS_MSG);
            this.isModelVisible = false;
            this.service.createInternalProgramsModel.reset();
          } else {
            this.message.error(AdminDocumentConstants.ERROR_INTERNAL_PROGRAM_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createInternalProgramsModel.reset();
          }
        },
      });
    }
  }

  updateInternalProgram(): void {
    if (this.service.editInternalProgramsModel.valid) {
      this.service.updateInternalProgram().subscribe({
        next: () => {
          this.message.success(AdminDocumentConstants.INTERNAL_PROGRAM_UPDATED_MSG);
          this.getData();
          this.handleCancel();
        },
        error: (err: any) => {
          if (err.status == 409) {
            this.message.error(AdminDocumentConstants.INTERNAL_PROGRAM_TYPE_EXISTS_MSG);
          }
          else {
            this.message.error(AdminDocumentConstants.ERROR_INTERNAL_PROGRAM_CREATED_MSG);
          }
        },
      });
    } else {
      Object.values(this.service.editInternalProgramsModel.controls).forEach((control) => {
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
    this.service.createInternalProgramsModel.reset();
    this.service.editInternalProgramsModel.reset();
  }
}
