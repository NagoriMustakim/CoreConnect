import { Proficiency } from './../../../../shared/interface/proficiency';
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
import { ProficiencyService } from '../../service/proficiency.service';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-proficiency',
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
    NzTypographyModule
  ],
  templateUrl: './proficiency.component.html',
  styleUrls: ['./proficiency.component.css', '/src/styles.css'],
})
export class ProficiencyComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  proficiencies: Proficiency[] = [];
  constructor(
    public service: ProficiencyService,
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
      .getProficiencyType()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.proficiencies = res;
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
      nzTitle: AdminDocumentConstants.PROFICIENCY_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteProficiencyType(id).subscribe((res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.PROFICIENCY_DELETED_MSG)
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createProficiencyModel.invalid) {
      Object.values(this.service.createProficiencyModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createProficiencyType().subscribe({
        next: (res) => {
          this.getData();
          this.message.success(AdminDocumentConstants.PROFICIENCY_CREATED_MSG);
          this.service.createProficiencyModel.reset();
          this.isModelVisible = false;
        },
        error: (err) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.PROFICIENCY_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_PROFICIENCY_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createProficiencyModel.reset();
          }
        },
      });
    }
  }

  updateProficiencyType(): void {
    if (this.service.editProficiencyModel.valid) {
      this.service.updateProficiencyType().subscribe({
        next: () => {
          this.message.success(AdminDocumentConstants.PROFICIENCY_UPDATED_MSG);
          this.getData();
          this.handleCancel();
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.PROFICIENCY_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_PROFICIENCY_CREATED_MSG);
          }
        },
      });
    } else {
      Object.values(this.service.editProficiencyModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    }
  }

  handleCancel() {
    this.isModelVisible = false;
    this.isEditModalVisible = false;
  }
}
