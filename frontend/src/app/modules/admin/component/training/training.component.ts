import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { concatMap } from 'rxjs';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Component, OnInit, Input } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import {
  FormControl,
  FormGroup,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { ITraining } from '../../../candidate/interfaces/ITraining';
import { TrainingService } from '../../service/training.service';
import { TrainingTypeService } from '../../../admin/service/training-type.service';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { TrainingNameService } from '../../../../shared/services/training-name.service';

@Component({
  selector: 'app-training',
  standalone: true,
  imports: [
    NzButtonModule,
    NzCardModule,
    NzTabsModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzIconModule,
    NzFlexModule,
    NzSpaceModule,
    NzImageModule,
    NzModalModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzUploadModule,
    CommonModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzTagModule,
    NzInputNumberModule,
    NzGridModule,
    NzEmptyModule,
    NzTypographyModule
  ],
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css', '/src/styles.css'],
})
export class TrainingComponent implements OnInit {
  @Input() userId: string = '';
  trainings: ITraining[] = [];
  trainingTypes: any;
  initLoading = true;
  isTrainingModalVisible = false;
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  startValue: Date | null = null;
  endValue: Date | null = null;
  trainingNames: any;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private msg: NzMessageService,
    private trainingService: TrainingService,
    private trainingTypeService: TrainingTypeService,
    private trainingNamesService: TrainingNameService

  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;

    this.formTraining = this.fb.group({
      userTrainingGuid: [''],
      trainingTypeGuid: ['', [required]],
      trainingNameGuid: ['', [required]],
      isTrainingActive: [false, [required]],
      trainingStartDate: ['', [required]],
      trainingEndDate: ['', [required]],
      userTrainingDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });


  }

  ngOnInit() {
    this.getTrainings();
  }

  getTrainings(): void {
    this.trainingService.getTrainings(this.userId).subscribe((response) => {
      this.trainings = response;
      this.initLoading = false;
      this.trainingTypeService.getTrainingType().subscribe((response) => {
        this.trainingTypes = response;
        this.trainingNamesService.getAllTrainings().subscribe((response: any) => {
          this.trainingNames = response;
        })
      });
    });
  }

  formTraining: FormGroup<{
    userTrainingGuid: FormControl<string>;
    trainingTypeGuid: FormControl<string>;
    trainingNameGuid: FormControl<string>;
    isTrainingActive: FormControl<boolean>;
    trainingStartDate: FormControl<string>;
    trainingEndDate: FormControl<string>;
    userTrainingDescription: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  showTrainingModal(training?: ITraining): void {
    if (training) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formTraining.patchValue(training);
      this.handleCurrentTrainingCheckbox(training.isTrainingActive);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isTrainingModalVisible = true;
  }

  handleTrainingModalOk(): void {
    if (this.formTraining.valid) {
      if (this.formTraining.value.userTrainingGuid === '') {
        this.trainingService
          .addTraining(this.userId, this.formTraining.value)
          .subscribe({
            next:
              (response: ITraining) => {
                this.getTrainings();
                this.formTraining.reset();
                this.isTrainingModalVisible = false;
                this.msg.success(sharedConstant.TRAINING_ADD)
                this.startValue = null;
                this.endValue = null;
              },
            error: () => this.msg.error(sharedConstant.TRAINING_ERROR_ADD)
          });
      } else {
        this.trainingService
          .updateTraining(this.userId, this.formTraining.value)
          .subscribe({
            next:
              (response: ITraining) => {
                let index = this.trainings.findIndex(
                  (training) =>
                    this.formTraining.value.userTrainingGuid == training.userTrainingGuid
                );

                this.trainings[index] = {
                  ...this.trainings[index],
                  ...response,
                };

                this.formTraining.reset();
                this.isTrainingModalVisible = false;
                this.msg.success(sharedConstant.TRAINING_UPDATE)
                this.startValue = null;
                this.endValue = null;
              },
            error: () => this.msg.error(sharedConstant.TRAINING_ERROR_UPDATE)
          });
      }
    } else {
      Object.values(this.formTraining.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleTrainingModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isTrainingModalVisible = false;
  }

  handleCurrentTrainingCheckbox(required: boolean): void {
    if (required) {
      this.formTraining.controls.trainingEndDate.clearValidators();
      this.formTraining.controls.trainingEndDate.markAsPristine();
    } else {
      this.formTraining.controls.trainingEndDate.setValidators(
        Validators.required
      );
      this.formTraining.controls.trainingEndDate.markAsDirty();
    }
    this.formTraining.controls.trainingEndDate.updateValueAndValidity();
  }

  handleAfterTrainingModalClose() {
    this.formTraining.reset();
    this.formTraining.controls.trainingEndDate.setValidators(
      Validators.required
    );
  }

  showTrainingDeleteConfirm(trainingId: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.TRAINING_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.trainingService
          .deleteTraining(this.userId, trainingId)
          .subscribe({
            next:
              () => {
                (this.trainings = this.trainings.filter(
                  (training) => training.userTrainingGuid !== trainingId
                ))
                this.msg.success(sharedConstant.TRAINING_DELETE)
              },
            error: () => this.msg.error(sharedConstant.TRAINING_ERROR_DELETE)
          }
          ),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue || !this.endValue) {
      return false;
    }
    return startValue.getTime() > this.endValue.getTime();
  };

  disabledEndDate = (endValue: Date): boolean => {
    if (!endValue || !this.startValue) {
      return false;
    }
    return endValue.getTime() <= this.startValue.getTime();
  };

  handleStartOpenChange(): void {
    if (this.formTraining.controls.trainingStartDate.value && this.formTraining.controls.trainingStartDate.value !== '') {
      this.startValue = new Date(
        this.formTraining.controls.trainingStartDate.value
      );
    }
    else
      this.startValue = null;
  }

  handleEndOpenChange(): void {
    if (this.formTraining.controls.trainingEndDate.value && this.formTraining.controls.trainingEndDate.value !== '') {
      this.endValue = new Date(
        this.formTraining.controls.trainingEndDate.value
      );
    }
    else
      this.endValue = null;
  }
}
