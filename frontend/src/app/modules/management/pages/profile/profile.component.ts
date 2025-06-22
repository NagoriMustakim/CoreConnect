import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Router } from '@angular/router';
import { KraComponent } from './../../components/kra/kra.component';
import { Component, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import {
  FormsModule,
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  NonNullableFormBuilder,
} from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule, NzUploadFile } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { HttpClient } from '@angular/common/http';
import { catchError, concatMap, of } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { SkillService } from '../../services/skill.service';
import { Skill } from '../../interfaces/Skill';
import { ExperienceComponent } from '../../components/experience/experience.component';
import { CertificationComponent } from '../../components/certification/certification.component';
import { ProjectComponent } from '../../components/project/project.component';
import { TrainingComponent } from '../../components/training/training.component';
import {
  City,
  Country,
  ICity,
  ICountry,
  IState,
  State,
} from 'country-state-city';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { BasicDetailService } from '../../services/basic-detail.service';
import { BusinessUnitService } from '../../../admin/service/business-unit.service';
import { PronounService } from '../../../admin/service/pronoun.service';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { ManagementRoutesConstants } from '../../constants/ManagementRoutesConstants';
import { LanguageComponent } from '../../components/language/language.component';
import { SkillComponent } from '../../components/skill/skill.component';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { environment } from '../../../../../environments/environment';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { OnlyNumberDirective } from '../../../../shared/directives/only-number.directive';

@Component({
  selector: 'app-profile',
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
    ExperienceComponent,
    KraComponent,
    CertificationComponent,
    ProjectComponent,
    TrainingComponent,
    LanguageComponent,
    NzEmptyModule,
    SkillComponent,
    NzTypographyModule,
    OnlyNumberDirective
  ],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css', '/src/styles.css'],
})
export class ProfileComponent implements OnInit {
  basicDetails = {} as IBasicDetail;
  businessUnits: any;
  pronouns: any;
  skills: Skill[] = [];
  countries: ICountry[] = [];
  states: IState[] = [];
  cities: ICity[] = [];
  selectedCountry: any;
  selectedState: any;
  avtarFallback = ManagementDocumentConstants.FALLBACK_AVATAR;
  businessUnitFallback = ManagementDocumentConstants.FALLBACK_BU;
  bannerFallback = ManagementDocumentConstants.FALLBACK_BANNER;
  profileImage: boolean = false;
  imageUrl = environment.imageUrl;

  constructor(
    private fb: NonNullableFormBuilder,
    private msg: NzMessageService,
    private skillService: SkillService,
    private router: Router,
    private basicDetailService: BasicDetailService,
    private businessUnitService: BusinessUnitService,
    private pronounService: PronounService,
    private eventEmitterService: EventEmitterService

  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formNomination = this.fb.group({
      internalProgramGuid: ['', [required]],
      justificationComment: ['', [required]],
    });

    this.formExportResume = this.fb.group({
      businessUnit: ['', [required]],
    });

    this.formBasicDetails = this.fb.group({
      id: [''],
      employeeCode: [''],
      firstName: ['', [required, minLength(2), maxLength(50), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      lastName: ['', [required, minLength(2), maxLength(50), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      pronounGuid: [''],
      phoneNumber: ['', [minLength(10), maxLength(10), pattern(ManagementValidationConstants.REGEX_PATTERN_DIGITS)]],
      emergencyContactNo: ['', [minLength(10), maxLength(10), pattern(ManagementValidationConstants.REGEX_PATTERN_DIGITS)]],
      businessUnitGuid: ['', [required]],
      country: [''],
      state: [''],
      city: [''],
      designationGuid: [''],
      birthDate: [''],
      about: ['', [maxLength(1000), pattern(ManagementValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      skypeId: ['',[pattern(ManagementValidationConstants.REGEX_SKYPE)]],
      linkedInUrl: ['',[pattern(ManagementValidationConstants.REGEX_URL)]]
    });

    this.basicDetailService
      .getBasicDetails()
      .pipe(
        concatMap((response) => {
          this.basicDetails = response;

          return this.businessUnitService.getBusinessUnits();
        })
      ).pipe(concatMap((response) => {
        this.businessUnits = response;

        return this.pronounService.getPronouns();
      }))
      .pipe(
        concatMap((response) => {
          this.pronouns = response;

          return this.skillService.getSkills('');
        })).subscribe((response) => {
          this.skills = response;
          this.eventEmitterService.callGetDetails();
        });
  }

  ngOnInit() {
    this.skillService.getSkills('').subscribe((response) => {
      this.skills = response;
    });
    this.countries = Country.getAllCountries();
  }

  initLoading = true;
  loadingMore = false;
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];

  //Modal variables

  isNominationModalVisible = false;
  isExportResumeModalVisible = false;
  isBasicDetailModalVisible = false;

  saveMsg: string = ManagementDocumentConstants.ADD_MSG;

  fileList: NzUploadFile[] = [];

  submitting = false;
  inputValue = '';

  previewImage: string | undefined = '';
  previewVisible = false;

  //Form models

  formNomination: FormGroup<{
    internalProgramGuid: FormControl<string>;
    justificationComment: FormControl<string>;
  }>;

  formExportResume: FormGroup<{
    businessUnit: FormControl<string>;
  }>;

  formBasicDetails: FormGroup<{
    id: FormControl<string>;
    employeeCode: FormControl<string>;
    firstName: FormControl<string>;
    lastName: FormControl<string>;
    pronounGuid: FormControl<string>;
    phoneNumber: FormControl<string>;
    emergencyContactNo: FormControl<string>;
    businessUnitGuid: FormControl<string>;
    country: FormControl<string>;
    state: FormControl<string>;
    city: FormControl<string>;
    designationGuid: FormControl<string>;
    birthDate: FormControl<string>;
    about: FormControl<string>;
    skypeId: FormControl<string>;
    linkedInUrl: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  uploadAvatar = (files: any) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const isJpgOrPng = fileToUpload.type === sharedConstant.IMAGE_JPEG || fileToUpload.type === sharedConstant.IMAGE_PNG;

    if (!isJpgOrPng) {
      this.msg.error(sharedConstant.IMAGE_VALIDATION_MESSAGE);
    } else {
      this.basicDetailService.updateAvatar(fileToUpload).subscribe({
        next:
          (response: any) => {
            this.basicDetailService.getBasicDetails().subscribe({
              next: (response: any) => {
                this.basicDetails = response;
                this.profileImage = false;
                this.eventEmitterService.callGetDetails();
                this.msg.success(sharedConstant.IMAGE_UPDATED)
              }
            })
          }
      });
    }
  }

  //Methods for showing models

  showNominationModal(msg: string): void {
    this.isNominationModalVisible = true;
    this.saveMsg = msg;
  }

  showExportResumeModal(): void {
    this.isExportResumeModalVisible = true;
  }

  showBasicDetailModal(basicDetail: IBasicDetail): void {
    this.getStatesByCountry(basicDetail.country);
    this.getCitiesByState(basicDetail.state);
    this.formBasicDetails.patchValue(basicDetail);
    this.isBasicDetailModalVisible = true;
  }

  edit(item: any): void {
    this.msg.success(item.email);
  }

  //methods for handling ok button click

  handleExportResumeModalOk(): void {
    if (this.formExportResume.valid) {
      this.router.navigateByUrl(ManagementRoutesConstants.RESUME);
      this.formExportResume.reset();
      this.isExportResumeModalVisible = false;
    } else {
      Object.values(this.formExportResume.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleBasicDetailsModalOk(): void {
    if (this.formBasicDetails.valid) {
      this.basicDetailService
        .updateBasicDetail(this.formBasicDetails.value)
        .subscribe({
          next:
            () => {
              this.basicDetailService
                .getBasicDetails()
                .subscribe((response) => (this.basicDetails = response));
              this.formBasicDetails.reset();
              this.isBasicDetailModalVisible = false;
              this.eventEmitterService.callGetDetails();
              this.msg.success(sharedConstant.BASIC_DETAIL_UPDATE)
            },
          error: () => this.msg.error(sharedConstant.BASIC_DETAIL_UPDATE_ERROR)
        });
    } else {
      Object.values(this.formBasicDetails.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  //methods for handling cancel button click

  handleNominationModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formNomination.reset();
    this.isNominationModalVisible = false;
  }

  handleExportResumeModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formExportResume.reset();
    this.isExportResumeModalVisible = false;
  }

  handleBasicDetailsModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formBasicDetails.reset();
    this.isBasicDetailModalVisible = false;
  }

  getStatesByCountry(value: string) {
    if (value) {
      this.formBasicDetails.controls.state.reset();
      this.formBasicDetails.controls.city.reset();
      this.selectedCountry = this.countries.find(
        (country) => country.name === value
      );
      this.states = State.getStatesOfCountry(this.selectedCountry?.isoCode);
    }
  }
  getCitiesByState(value: string) {
    if (value) {
      this.formBasicDetails.controls.city.reset();
      this.selectedState = this.states.find((state) => state.name === value);
      this.cities = City.getCitiesOfState(
        this.selectedCountry.isoCode,
        this.selectedState.isoCode
      );
    }
  }

  DeleteProfileImage(profilePhotoName: string): void {
    this.basicDetailService.deleteAvatar().subscribe({
      next:
        (response: any) => {
          this.basicDetailService.getBasicDetails().subscribe({
            next: (response: any) => {
              this.basicDetails = response;
              this.profileImage = false;
              this.eventEmitterService.callGetDetails();
              this.msg.success(sharedConstant.IMAGE_DELETED)
            }
          })
        }
    });
  }

  showProfileImageModel(): void {
    this.profileImage = true;
  }

  onCancel(): void {
    this.profileImage = false;
  }
}

//validators
export type MyErrorsOptions = { en: string } & Record<string, NzSafeAny>;
export type MyValidationErrors = Record<string, MyErrorsOptions>;
