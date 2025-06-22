import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { SkillComponent } from './../../components/skill/skill.component';
import { ActivatedRoute, Router } from '@angular/router';
import { NoiminationService } from './../../services/noimination.service';
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
  ValidatorFn,
  AbstractControl,
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
import { concatMap } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { Skill } from '../../interfaces/Skill';
import { ExperienceComponent } from '../../components/experience/experience.component';
import { CertificationComponent } from '../../components/certification/certification.component';
import { ProjectComponent } from '../../components/project/project.component';
import { TrainingComponent } from '../../components/training/training.component';
import { City, Country, ICity, ICountry, IState, State } from 'country-state-city';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { BasicDetailService } from '../../services/basic-detail.service';
import { BusinessUnitService } from '../../../admin/service/business-unit.service';
import { PronounService } from '../../../admin/service/pronoun.service';
import { LanguageComponent } from '../../components/language/language.component';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { RoutesConstants } from '../../constants/RoutesConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { float } from 'html2canvas/dist/types/css/property-descriptors/float';
import { AdminValidationConstants } from '../../../admin/constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../../admin/constants/AdminDocumentConstants';
import { AuthService } from '../../../auth/service/auth.service';
import { environment } from '../../../../../environments/environment';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { OnlyNumberDirective } from '../../../../shared/directives/only-number.directive';
import { DesginationService } from '../../../../shared/services/designation.service';

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
    LanguageComponent,
    KraComponent,
    CertificationComponent,
    ProjectComponent,
    TrainingComponent,
    SkillComponent,
    NzEmptyModule,
    NzTypographyModule,
    OnlyNumberDirective
  ],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css', '/src/styles.css']
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
  userId: any;
  startValue: Date | null = null;
  showStates: boolean = false;
  showCities: boolean = false;
  isResetModalVisible: boolean = false;
  passwordVisible: boolean = false;
  confirmPasswordVisible: boolean = false;
  profileImage: boolean = false;
  imageUrl = environment.imageUrl;

  validateForm: FormGroup<{
    password: FormControl<string>;
    checkPassword: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private msg: NzMessageService,
    private router: Router,
    private nominationService: NoiminationService,
    private basicDetailService: BasicDetailService,
    private businessUnitService: BusinessUnitService,
    private pronounService: PronounService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private eventEmitterService: EventEmitterService,
    private DesignationService: DesginationService
  ) {
    const { required, maxLength, minLength, pattern, passwordRegister } = CustomValidators;

    this.formBasicDetails = this.fb.group({
      id: [''],
      employeeCode: [''],
      firstName: ['', [required, minLength(2), maxLength(50), pattern(ValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      lastName: ['', [required, minLength(2), maxLength(50), pattern(ValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      pronounGuid: [''],
      phoneNumber: ['', [minLength(10), maxLength(10), pattern(ValidationConstants.REGEX_PATTERN_DIGITS)]],
      emergencyContactNo: ['', [minLength(10), maxLength(10), pattern(ValidationConstants.REGEX_PATTERN_DIGITS)]],
      businessUnitGuid: ['', [required]],
      country: [''],
      state: [''],
      city: [''],
      designationGuid: [''],
      birthDate: [''],
      about: ['', [maxLength(1000), pattern(ValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      skypeId: ['', [pattern(ValidationConstants.REGEX_SKYPE)]],
      linkedInUrl: ['', [pattern(ValidationConstants.REGEX_URL)]]
    });

    this.validateForm = this.fb.group({
      password: ['', [required, minLength(8), passwordRegister()]],
      checkPassword: ['', [required, this.confirmationValidator]],
    });

    this.basicDetailService.getBasicDetails().pipe(concatMap((response: IBasicDetail) => {
      this.basicDetails = response;

      return this.businessUnitService.getBusinessUnits();
    })).pipe(concatMap((response) => {
      this.businessUnits = response;

      return this.pronounService.getPronouns();
    })).subscribe(response => {
      this.pronouns = response
      this.eventEmitterService.callGetDetails();
    });
  }

  ngOnInit() {
    this.route.queryParams.subscribe((param: any) => {
      this.userId = param.userId;
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

  saveMsg: string = DocumentConstants.ADD_MSG;

  fileList: NzUploadFile[] = [];

  submitting = false;
  inputValue = '';
  avtarFallback = DocumentConstants.FALLBACK_AVATAR;
  businessUnitFallback = DocumentConstants.FALLBACK_BU;
  bannerFallback = DocumentConstants.FALLBACK_BANNER;

  //Form model

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
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  //Method for showing modal

  showBasicDetailModal(basicDetail: IBasicDetail): void {
    this.getStatesByCountry(basicDetail.country);
    this.getCitiesByState(basicDetail.state);
    this.formBasicDetails.patchValue(basicDetail);
    this.isBasicDetailModalVisible = true;
  }

  edit(item: any): void {
    this.msg.success(item.email);
  }

  //method for handling ok button click

  handleBasicDetailsModalOk(): void {
    if (this.formBasicDetails.valid) {
      this.basicDetailService.updateBasicDetail(this.formBasicDetails.value).subscribe(() => {
        this.basicDetailService.getBasicDetails().subscribe({
          next:
            (response: IBasicDetail) => {
              this.basicDetails = response;
              this.eventEmitterService.callGetDetails();
              this.msg.success(sharedConstant.BASIC_DETAIL_UPDATE);
            },
          error: (err: any) => {
            if (err.error)
              this.msg.error(sharedConstant.BASIC_DETAIL_UPDATE_ERROR)
          }
        });

        this.formBasicDetails.reset();
        this.isBasicDetailModalVisible = false;
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

  confirmationValidator: ValidatorFn = (
    control: AbstractControl
  ): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  };

  //methods for handling cancel button click

  handleBasicDetailsModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formBasicDetails.reset();
    this.isBasicDetailModalVisible = false;
  }

  //success and error message

  getStatesByCountry(value: string) {
    if (value) {
      this.formBasicDetails.controls.state.reset();
      this.formBasicDetails.controls.city.reset();
      this.selectedCountry = this.countries.find((country) => country.name === value);
      this.states = State.getStatesOfCountry(this.selectedCountry?.isoCode);
      this.showStates = true;
    }
  }

  getCitiesByState(value: string) {
    if (value) {
      this.formBasicDetails.controls.city.reset();
      this.selectedState = this.states.find((state) => state.name === value);
      this.cities = City.getCitiesOfState(this.selectedCountry.isoCode, this.selectedState.isoCode);
      this.showCities = true;
    }
  }

  uploadAvatar = (files: any) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const isJpgOrPng = fileToUpload.type === sharedConstant.IMAGE_JPEG || fileToUpload.type === sharedConstant.IMAGE_PNG|| fileToUpload.type === sharedConstant.IMAGE_GIF;

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

  handleChangePassword() {
    if (this.validateForm.valid) {
      const newPassword = this.validateForm.get(AdminDocumentConstants.PASSWORD)?.value;
      if (newPassword) {
        this.authService.changePassword(this.authService.getUserId(), newPassword).subscribe({
          next: (res: any) => {
            this.validateForm.reset();
            this.msg.success(AdminDocumentConstants.PASSWORD_CHANGED_MSG);
            this.isResetModalVisible = false;
          },
          error: (err) => this.msg.error(AdminDocumentConstants.ERROR_PASSWORD_CHANGED_MSG),
        });
      }
    }
  }

  updateConfirmValidator(): void {
    Promise.resolve().then(() =>
      this.validateForm.controls.checkPassword.updateValueAndValidity()
    );
  }

  openResetModal() {
    this.isResetModalVisible = true;
  }

  handleCancel() {
    this.isResetModalVisible = false;
  }

  showProfileImageModel(): void {
    this.profileImage = true;
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

  onCancel(): void {
    this.profileImage = false;
  }
}

//validators
export type MyErrorsOptions = { en: string } & Record<string, NzSafeAny>;
export type MyValidationErrors = Record<string, MyErrorsOptions>;
