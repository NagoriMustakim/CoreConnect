import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzUploadModule, NzUploadFile } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { CommonModule } from '@angular/common';
import { NzCardModule } from 'ng-zorro-antd/card';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { GiftProgramService } from '../../services/gift-program.service';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { IGiftProgram } from '../../interfaces/IGiftProgram';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { Status } from '../../../../shared/enums/status.enum';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { DesginationService } from '../../../../shared/services/designation.service';
import { concatMap } from 'rxjs';
import { BasicDetailService } from '../../services/basic-detail.service';

const getBase64 = (file: File): Promise<string | ArrayBuffer | null> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

@Component({
  selector: 'app-gift-program',
  standalone: true,
  imports: [
    NzIconModule,
    NzFormModule,
    CommonModule,
    NzCardModule,
    NzUploadModule,
    NzModalModule,
    NzInputModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzSelectModule,
    NzCheckboxModule,
    NzListModule,
    NzTabsModule,
    NzTimelineModule,
    NzSpaceModule,
    NzAvatarModule,
    RouterOutlet,
    RouterLink,
    NzFlexModule,
    NzDatePickerModule,
    NzSkeletonModule,
    NzTagModule,
    NzEmptyModule
  ],
  templateUrl: './gift-program.component.html',
  styleUrls: ['./gift-program.component.css', '/src/styles.css'],
})


export class GiftProgramComponent {

  previewImage: string | undefined = '';
  previewVisible = false;
  isGiftStatusModalVisible = false;
  isGiftModalVisible = false;
  initLoading = true;
  loadingMore = false;
  applicationrequests: any[] = [];
  giftApplication = {} as IGiftProgram;
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  currentDesignation: any;
  designations: any[]=[];
  giftForm: FormGroup<{
    currentDesignationTitle: FormControl<string>;
    desiredDesignationGuid: FormControl<string>;
    gapsIdentified: FormControl<string>;
    planofAction: FormControl<string>;
    targetAchievementDate: FormControl<string>;
    isSupportRequired: FormControl<boolean>;
    endResult: FormControl<string>;
    majorAchievements: FormControl<string>;
    learningAndTransistionProcess: FormControl<string>;
    careerGrowthContribution: FormControl<string>;
    workRelatedTrainingAndCertifications: FormControl<string>;
  }>;
  currentDesignationGuid:string='';
  constructor(
    private fb: NonNullableFormBuilder,
    private giftService: GiftProgramService,
    private message: NzMessageService,
    private eventEmitterService: EventEmitterService,
    private DesignationService: DesginationService,
    private basicDetailService: BasicDetailService,
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.giftForm = this.fb.group({
      currentDesignationTitle: ['', [required]],
      desiredDesignationGuid: ['', [required]],
      gapsIdentified: [
        '',
        [required, minLength(20), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)],
      ],
      planofAction: [
        '',
        [required, minLength(20), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)],
      ],
      targetAchievementDate: ['', [required]],
      isSupportRequired: [false, [required]],
      endResult: [
        '',
        [required, minLength(20), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)],
      ],
      majorAchievements: [
        '',
        [required, minLength(20), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)],
      ],
      learningAndTransistionProcess: ['', [required, minLength(2), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)]],
      careerGrowthContribution: ['', [required, minLength(2), maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)]],
      workRelatedTrainingAndCertifications: ['', [required, minLength(2), maxLength(100), pattern(ValidationConstants.REGEX_PATTERN_ALPHANUMERICS_SPACE_DOT_HYPHEN)]],
    });
    // this.giftForm.controls.currentDesignationTitle.disable();
    this.LoadData();
  }

  LoadData() {
    this.giftService.getGiftDetails().subscribe((response) => {
      this.applicationrequests = response;
      this.initLoading = false;
      this.eventEmitterService.callGetDetails();
    })
  }
  handlePreview = async (file: NzUploadFile): Promise<void> => {
    if (!file.url && !file['preview']) {
      file['preview'] = await getBase64(file.originFileObj!);
    }
    this.previewImage = file.url || file['preview'];
    this.previewVisible = true;
  };

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  submitForm(): void {
    console.log(this.giftForm.value);

    if (this.giftForm.valid) {
      this.giftService
        .postGiftForm(this.giftForm.value)
        .subscribe({
          next: (response: IGiftProgram) => {
            this.applicationrequests.push(response);
            this.giftForm.reset();
            this.isGiftModalVisible = false;
            this.message.success(DocumentConstants.GIFT_FORM_SUBMITTED_MSG)
          },
          error: (error: any) => {
            this.message.error(error.error.message)
          }
        })
    } else {
      Object.values(this.giftForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  getStatus(status: Status): string {
    return Status[status];
  }

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }

  showGiftForm(): void {
    this.basicDetailService.getBasicDetails().pipe(concatMap((response: any) => {
      this.currentDesignation = response.designation;
      this.currentDesignationGuid = response.designationGuid;
      return this.DesignationService.getAllDesignation()
    }))
      .subscribe((response: any) => {
        this.designations = response.list
        const currentDesignation = this.designations.filter(des=>des.designationGuid==this.currentDesignationGuid);
        this.designations = this.designations.filter(des=>des.designationLevel>currentDesignation[0].designationLevel)
        console.log(this.designations);

      })
    this.isGiftModalVisible = true;


  }

  showGiftStatus(giftApplication: IGiftProgram): void {
    this.giftApplication = giftApplication;
    this.isGiftStatusModalVisible = true;
  }

  handleGiftModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftModalVisible = false;
    this.giftForm.reset()
  }

  handleGiftStatusBack(e: MouseEvent): void {
    e.preventDefault();
    this.isGiftStatusModalVisible = false;
  }

  createMessage(type: string, mes: string): void {
    this.message.create(type, mes);
  }
}
