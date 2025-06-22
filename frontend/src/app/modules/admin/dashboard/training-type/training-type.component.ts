import { TrainingType } from './../../interfaces/trainingType';
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
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { TrainingTypeService } from '../../service/training-type.service';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-training-type',
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
  templateUrl: './training-type.component.html',
  styleUrls: ['./training-type.component.css', '/src/styles.css']
})
export class TrainingTypeComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  trainingTypes: TrainingType[] = [];

  constructor(
    public service: TrainingTypeService,
    private message: NzMessageService,
    private modal: NzModalService
  ) { }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service
      .getTrainingType()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.trainingTypes = res;
        console.log(this.trainingTypes);
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

  handleUpdate() {
    if (this.service.editTrainingTypeModel.valid) {
      this.service.updateTrainingType().subscribe({
        next: () => {
          this.message.success(AdminDocumentConstants.TRAINING_TYPE_CREATED_MSG);
          this.getData();
          this.handleCancel();
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.TRAINING_TYPE_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_TRAINING_TYPE_UPDATED_MSG);
          }
        },
      });
    } else {
      Object.values(this.service.editTrainingTypeModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  delete(id: any) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.TRAINING_TYPE_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteTrainingType(id).subscribe((res: any) => {
          this.getData();
          this.message.success(AdminDocumentConstants.TRAINING_TYPE_DELETED_MSG)
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createTrainingTypeModel.invalid) {
      Object.values(this.service.createTrainingTypeModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createTrainingType().subscribe({
        next: (res) => {
          this.getData();
          this.message.success(AdminDocumentConstants.TRAINING_TYPE_CREATED_MSG);
          this.service.createTrainingTypeModel.reset();
          this.isModelVisible = false;
        },
        error: (err) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.TRAINING_TYPE_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_TRAINING_TYPE_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createTrainingTypeModel.reset();
          }
        },
      });
    }
  }

  handleCancel() {
    this.isModelVisible = false;
    this.isEditModalVisible = false;
  }
}
