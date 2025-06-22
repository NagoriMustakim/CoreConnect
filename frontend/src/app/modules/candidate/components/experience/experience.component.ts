import { ComponyService } from './../../../admin/service/compony.service';
import { DesignationService } from './../../../admin/service/designation.service';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { LoginComponent } from './../../../auth/login/login.component';
import { Component, Input, OnInit } from '@angular/core';
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
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { ExperienceService } from '../../services/experience.service';
import { IExperience } from '../../interfaces/IExperience';
import { EmploymentTypeService } from '../../../admin/service/employment-type.service';
import { LocationTypeService } from '../../../admin/service/location-type.service';
import {
  City,
  Country,
  ICity,
  ICountry,
  IState,
  State,
} from 'country-state-city';
import { concatMap } from 'rxjs';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { DocumentConstants } from '../../constants/DocumentConstants';

@Component({
  selector: 'app-experience',
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
  templateUrl: './experience.component.html',
  styleUrls: ['./experience.component.css', '/src/styles.css'],
})
export class ExperienceComponent implements OnInit {
  @Input() userId: string = '';
  experiences: IExperience[] = [];
  designations: any;
  companies:any;
  employmentTypes: any;
  locationTypes: any;
  countries: ICountry[] = [];
  states: IState[] = [];
  cities: ICity[] = [];
  selectedCountry: any;
  selectedState: any;
  startValue: Date | null = null;
  endValue: Date | null = null;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private experienceService: ExperienceService,
    private employmentTypeService: EmploymentTypeService,
    private locationTypeService: LocationTypeService,
    private designationService: DesignationService,
    private companyService: ComponyService
  ) {
    const { required, maxLength, pattern } = CustomValidators;

    this.formExperience = this.fb.group({
      experienceGuid: [''],
      employmentTypeGuid: [''],
      locationTypeGuid: [''],
      designationGuid: ['',[required]],
      companyGuid: ['',[required]],
      country: [''],
      state: [''],
      city: [''],
      experienceStartDate: ['', [required]],
      experienceEndDate: ['', [required]],
      isExperienceActive: [false, [required]],
      experienceDescription: ['', [maxLength(500), pattern(ValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      employmentTypeName: [''],
    });
  }

  ngOnInit() {
    this.experienceService
      .getExperiences(this.userId)
      .pipe(
        concatMap((response) => {
          this.experiences = response;

          return this.designationService.getDesignations(0,0);
        }))
        .pipe(
          concatMap((response:any)=>{
            this.designations = response.list;
          return this.employmentTypeService.getEmploymentTypes();
        }))
      .pipe(
        concatMap((response) => {
          this.employmentTypes = response;
          return this.companyService.getComponies(0,0);
        }))
        .pipe(concatMap((response:any)=>{
          this.companies = response.list
          return this.locationTypeService.getLocaitonTypes();
        })
      )
      .subscribe((response: any) => (this.locationTypes = response));

    this.countries = Country.getAllCountries();
  }

  isExperienceModalVisible = false;
  saveMsg: string = DocumentConstants.ADD_MSG;

  formExperience: FormGroup<{
    experienceGuid: FormControl<string>;
    employmentTypeGuid: FormControl<string>;
    locationTypeGuid: FormControl<string>;
    designationGuid: FormControl<string>;
    companyGuid: FormControl<string>;
    country: FormControl<string>;
    state: FormControl<string>;
    city: FormControl<string>;
    experienceStartDate: FormControl<string>;
    experienceEndDate: FormControl<string>;
    isExperienceActive: FormControl<boolean>;
    experienceDescription: FormControl<string>;
    employmentTypeName: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  showExperienceModal(experience?: IExperience): void {
    if (experience) {
      this.saveMsg = DocumentConstants.EDIT_MSG;
      this.getStatesByCountry(experience.country);
      this.getCitiesByState(experience.state);
      this.formExperience.patchValue(experience);
      this.handleCurrentRoleCheckbox(experience.isExperienceActive);
      this.handleStartOpenChange();
      this.handleEndOpenChange();
      this.disabledStartDate(new Date(experience!.experienceStartDate));
      this.disabledEndDate(new Date(experience!.experienceEndDate));

      Object.values(this.formExperience.controls).forEach(control => {
        control.value === null && '';
      });

    } else {
      this.saveMsg = DocumentConstants.ADD_MSG;
    }
    this.isExperienceModalVisible = true;
  }

  handleExperienceModalOk(): void {
    if (this.formExperience.valid) {
      if (this.formExperience.value.experienceGuid === '') {
        this.experienceService
          .addExperience(this.formExperience.value)
          .subscribe({
            next:
              (response: IExperience) => {
                this.experiences.unshift(response);
                this.formExperience.reset();
                this.isExperienceModalVisible = false;
                this.message.success(sharedConstant.EXPERIENCE_ADD);
                this.startValue = null;
                this.endValue = null;
              },
            error: (error: any) => {
              this.message.error(sharedConstant.EXPERIENCE_ERROR_ADD);
            }
          });
      } else {
        this.experienceService
          .updateExperience(this.formExperience.value)
          .subscribe({
            next:
              (response: IExperience) => {
                let index = this.experiences.findIndex(
                  (experience) =>
                    this.formExperience.value.experienceGuid ==
                    experience.experienceGuid
                );

                this.experiences[index] = {
                  ...this.experiences[index],
                  ...response,
                };

                this.formExperience.reset();
                this.isExperienceModalVisible = false;
                this.message.success(sharedConstant.EXPERIENCE_UPDATE);
                this.startValue = null;
                this.endValue = null;
              },
            error: (err: any) => {
              this.message.success(sharedConstant.EXPERIENCE_ERROR_UPDATE)
            }
          });
      }
    } else {
      Object.values(this.formExperience.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleExperienceModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isExperienceModalVisible = false;
    this.startValue = null;
    this.endValue = null;
  }

  handleCurrentRoleCheckbox(required: boolean): void {
    if (required) {
      this.formExperience.controls.experienceEndDate.clearValidators();
      this.formExperience.controls.experienceEndDate.markAsPristine();
    } else {
      this.formExperience.controls.experienceEndDate.setValidators(
        Validators.required
      );
      this.formExperience.controls.experienceEndDate.markAsDirty();
    }
    this.formExperience.controls.experienceEndDate.updateValueAndValidity();
  }

  handleAfterExperienceModalClose() {
    this.formExperience.reset();
    this.formExperience.controls.experienceEndDate.setValidators(
      Validators.required
    );
  }

  showExperienceDeleteConfirm(experienceId: string): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.EXPERIENCE_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.experienceService
          .deleteExperience(experienceId)
          .subscribe({
            next:
              () => {
                (this.experiences = this.experiences.filter(
                  (experience) => experience.experienceGuid !== experienceId
                ))
                this.message.success(sharedConstant.EXPERIENCE_DELETE)
              },
            error: (err: any) => {
              this.message.error(sharedConstant.EXPERIENCE_ERROR_DELETE)
            }
          }),
      nzCancelText: DocumentConstants.NO_KEEP,
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
    if (this.formExperience.controls.experienceEndDate.value && this.formExperience.controls.experienceEndDate.value !== '') {
      this.startValue = new Date(this.formExperience.controls.experienceEndDate.value);
    } else
      this.startValue = null;
  }

  handleEndOpenChange(): void {
    if (this.formExperience.controls.experienceEndDate.value && this.formExperience.controls.experienceEndDate.value !== '') {
      this.endValue = new Date(this.formExperience.controls.experienceEndDate.value);
    } else
      this.endValue = null;
  }

  getStatesByCountry(value: string) {
    if (value) {
      this.selectedCountry = this.countries.find(
        (country) => country.name === value
      );
      this.states = State.getStatesOfCountry(this.selectedCountry?.isoCode);
    }
  }

  getCitiesByState(value: string) {
    if (value) {
      this.selectedState = this.states.find((state) => state.name === value);
      this.cities = City.getCitiesOfState(
        this.selectedCountry.isoCode,
        this.selectedState.isoCode
      );
    }
  }
}
