import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzModalComponent, NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';
import { CommonModule } from '@angular/common';
import { TrainingsNamesService } from '../../service/trainings-names.service';
import { NzListComponent, NzListModule } from 'ng-zorro-antd/list';
import { NzEmptyComponent } from 'ng-zorro-antd/empty';
import { ITraining } from '../../interfaces/ITraining';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzSpaceModule } from 'ng-zorro-antd/space';
@Component({
  selector: 'app-training',
  standalone: true,
  imports: [NzFlexModule, NzIconModule, NzButtonModule, NzModalComponent, NzModalModule, ReactiveFormsModule, NzInputModule, NzFormModule, FormsModule, CommonModule, NzListComponent, NzEmptyComponent, NzTypographyModule, NzListModule, NzTableModule,NzSpaceModule, NzTypographyModule],
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css','/src/styles.css'],
})
export class TrainingComponent implements OnInit {
  isModelVisible = false;
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  tranings: ITraining[] = [];
  pageSize = 10;
  total = 0;

  fromTraining: FormGroup<{
    trainingGuid: FormControl<string>,
    trainingTitle: FormControl<string>,
    trainingDescription: FormControl<string>
  }>

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  constructor(private fb: NonNullableFormBuilder, private msg: NzMessageService, private trainingNameService: TrainingsNamesService, private modal: NzModalService) {
    const { required, maxLength, pattern } = CustomValidators;
    this.fromTraining = this.fb.group({
      trainingGuid: [''],
      trainingTitle: ['', [required, maxLength(100), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE)]],
      trainingDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    })

  }
  ngOnInit(): void {
    this.getAllTranings();
  }
  getAllTranings(pageNumber: number = 1, pageSize: number = 10) {
    this.trainingNameService.getAllTrainings(pageNumber, pageSize).subscribe((response: any) => {
      this.tranings = response.list;
      this.total = response.count;
    })
  }

  showTrainingModal(training?: ITraining) {
    if (training) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.fromTraining.patchValue(training);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isModelVisible = true;
  }
  handleTrainingModalOk() {
    if (this.fromTraining.valid) {
      if (this.fromTraining.value.trainingGuid === '') {
        this.trainingNameService.CreateTrainings(this.fromTraining.value).subscribe({
          next:
            (response: ITraining) => {
              this.tranings.unshift(response)
              this.isModelVisible = false;
              this.getAllTranings()
              this.msg.success(AdminDocumentConstants.TRAINING_ADD)
              this.fromTraining.reset();
            },
          error: (err: any) => {
            if (err.status === 409) {
              this.msg.error(AdminDocumentConstants.TRAINING_ALREADY_EXISTS)
            } else {
              this.msg.error(AdminDocumentConstants.TRAINING_ERROR_ADD)
              this.isModelVisible = false;
            }
          }
        })
      } else {
        this.trainingNameService.UpdateTrainings(this.fromTraining.value).subscribe({
          next:
            (response: ITraining) => {
              let index = this.tranings.findIndex(
                (training) => this.fromTraining.value.trainingGuid == training.trainingGuid
              );

              this.tranings[index] = {
                ...this.tranings[index],
                ...response
              }

              this.fromTraining.reset();
              this.isModelVisible = false;
              this.msg.success(AdminDocumentConstants.TRAINING_UPDATE)
            },
          error: (err: any) => {
            if (err.status === 409) {
              this.msg.error(AdminDocumentConstants.TRAINING_ALREADY_EXISTS)
            } else {
              this.msg.error(AdminDocumentConstants.TRAINING_ERROR_UPDATE)
              this.isModelVisible = false;
            }
          }
        })
      }
    } else {
      Object.values(this.fromTraining.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
  showModal() {
    this.isModelVisible = true;
  }

  handleCancel() {
    this.isModelVisible = false;
    this.fromTraining.reset();
  }

  delete(trainingGuid: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.TRAINING_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.trainingNameService.DeleteTrainings(trainingGuid).subscribe({
          next:
            () => {
              (this.tranings = this.tranings.filter(
                (training) => training.trainingGuid !== trainingGuid
              ))
              this.getAllTranings()
              this.msg.success(AdminDocumentConstants.TRAINING_DELETE);
            },
          error: () => this.msg.error(AdminDocumentConstants.TRAINING_ERROR_DELETE)

        })
    })
  }
}
