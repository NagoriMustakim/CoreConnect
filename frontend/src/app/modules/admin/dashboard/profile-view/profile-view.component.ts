import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AuthService } from './../../../auth/service/auth.service';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzInputModule } from 'ng-zorro-antd/input';
import { PronounService } from './../../service/pronoun.service';
import { BusinessUnitService } from './../../service/business-unit.service';
import { concatMap } from 'rxjs';
import { IBasicDetail } from './../../../candidate/interfaces/IBasicDetail';
import { BasicDetailService } from './../../service/basic-detail.service';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import {
  NonNullableFormBuilder,
  FormGroup,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  AbstractControl,
} from '@angular/forms';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { SkillComponent } from './../../component/skill/skill.component';
import { TrainingComponent } from '../../../admin/component/training/training.component';
import { ProjectComponent } from '../../../admin/component/project/project.component';
import { CertificationsComponent } from '../../component/certifications/certifications.component';
import { CommonModule, Location } from '@angular/common';
import { ExperienceComponent } from '../../component/experience/experience.component';
import { KraComponent } from './../../component/kra/kra.component';
import { LanguageComponent } from './../../component/language/language.component';
import { ManageUserService } from './../../service/manage-user.service';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzCardModule } from 'ng-zorro-antd/card';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import {
  City,
  Country,
  ICity,
  ICountry,
  IState,
  State,
} from 'country-state-city';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzListModule } from 'ng-zorro-antd/list';
import { CommentComponent } from '../../component/comment/comment.component';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzResultModule } from 'ng-zorro-antd/result';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { environment } from '../../../../../environments/environment';
import { NzEmptyModule } from 'ng-zorro-antd/empty';

import { AdminRoutesConstants } from '../../constants/AdminRoutesConstants';
import { OnlyNumberDirective } from '../../../../shared/directives/only-number.directive';
import { DesginationService } from '../../../../shared/services/designation.service';
@Component({
  selector: 'app-profile-view',
  standalone: true,
  imports: [
    NzCardModule,
    NzFlexModule,
    NzGridModule,
    NzCommentModule,
    NzTabsModule,
    LanguageComponent,
    KraComponent,
    ExperienceComponent,
    CertificationsComponent,
    ProjectComponent,
    TrainingComponent,
    CommonModule,
    SkillComponent,
    NzInputModule,
    NzModalModule,
    NzSkeletonModule,
    NzButtonModule,
    FormsModule,
    NzAvatarModule,
    NzIconModule,
    NzSpaceModule,
    NzSelectModule,
    ReactiveFormsModule,
    NzFormModule,
    NzImageModule,
    NzDatePickerModule,
    NzListModule,
    CommentComponent,
    NzTypographyModule,
    NzResultModule,
    NzPageHeaderModule,
    NzEmptyModule,
    OnlyNumberDirective
  ],
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css', '/src/styles.css'],
})
export class ProfileViewComponent implements OnInit {
  userId: string = '';
  user!: any;
  isBasicDetailModalVisible = false;
  basicDetails = {} as IBasicDetail;
  countries: ICountry[] = [];
  states: IState[] = [];
  cities: ICity[] = [];
  selectedCountry: any;
  selectedState: any;
  businessUnits: any;
  pronouns: any;
  inputValue = '';
  submitting = false;
  initLoading = true;
  loadingMore = false;
  data: any[] = [];
  isResetModalVisible = false;
  passwordVisible = false;
  confirmPasswordVisible = false;
  profileImage: boolean = false;
  notFound = false;
  imageUrl = environment.imageUrl;
  canGoBack: boolean = false;
  isLoading = true
  validateForm: FormGroup<{
    password: FormControl<string>;
    checkPassword: FormControl<string>;
  }>;
  designations: any;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private route: ActivatedRoute,
    private msg: NzMessageService,
    private managerUserService: ManageUserService,
    private basicDetailService: BasicDetailService,
    private businessUnitService: BusinessUnitService,
    private pronounService: PronounService,
    private authService: AuthService,
    private router: Router,
    private location: Location,
    private DesignationService: DesginationService
  ) {
    this.canGoBack = !!(this.router.getCurrentNavigation()?.previousNavigation);

    const { required, maxLength, minLength, pattern, passwordRegister } =
      CustomValidators;

    this.validateForm = this.fb.group({
      password: ['', [required, minLength(8), passwordRegister()]],
      checkPassword: ['', [required, this.confirmationValidator]],
    });

    this.formBasicDetails = this.fb.group({
      id: [''],
      employeeCode: [''],
      firstName: ['', [required, minLength(2), maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      lastName: ['', [required, minLength(2), maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      pronounGuid: [''],
      phoneNumber: ['', [minLength(10), maxLength(10), pattern(AdminValidationConstants.REGEX_PATTERN_DIGITS)]],
      emergencyContactNo: ['', [minLength(10), maxLength(10), pattern(AdminValidationConstants.REGEX_PATTERN_DIGITS)]],
      businessUnitGuid: ['', [required]],
      country: [''],
      state: [''],
      city: [''],
      designationGuid: [''],
      birthDate: [''],
      joiningDate: [''],
      about: ['', [maxLength(1000), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      skypeId: ['', [pattern(AdminValidationConstants.REGEX_SKYPE)]],
      linkedInUrl: ['', [pattern(AdminValidationConstants.REGEX_URL)]]
    });


  }

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
    joiningDate: FormControl<string>;
    about: FormControl<string>;
    skypeId: FormControl<string>;
    linkedInUrl: FormControl<string>;
  }>;

  avtarFallback = AdminDocumentConstants.FALLBACK_AVATAR;
  businessUnitFallback = AdminDocumentConstants.FALLBACK_BU;
  bannerFallback = AdminDocumentConstants.FALLBACK_BANNER;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit(): void {
    this.countries = Country.getAllCountries();
    this.loadUserDetails();
  }

  loadUserDetails(): void {
    let id = this.route.snapshot.paramMap.get(AdminDocumentConstants.ID);
    if (window !== undefined && id !== null) {
      this.userId = id;
      window.localStorage.setItem(AdminDocumentConstants.USERID, id);
    }
    this.basicDetailService
      .getUserBasicDetails(this.userId)
      .pipe(
        concatMap((response: any) => {
          this.basicDetails = response;

          return this.businessUnitService.getBusinessUnits();
        }),
        concatMap((response: any) => {
          this.businessUnits = response;

          return this.pronounService.getPronouns();
        })
      )
      .subscribe((response: any) => {
        this.pronouns = response;
        this.isLoading = false; // Set loading to false once data is fetched
        this.DesignationService.getAllDesignation().subscribe((response: any) => {
          this.designations = response.list;
        })
      });
  }

  showBasicDetailModal(basicDetail: IBasicDetail): void {
    this.getStatesByCountry(basicDetail.country);
    this.getCitiesByState(basicDetail.state);
    this.formBasicDetails.patchValue(basicDetail);
    this.isBasicDetailModalVisible = true;
  }

  handleBasicDetailsModalOk(): void {
    if (this.formBasicDetails.valid) {
      this.basicDetailService
        .updateuserBasicDetails(this.formBasicDetails.value, this.userId)
        .subscribe({
          next: () => {
            this.basicDetailService.getUserBasicDetails(this.userId).subscribe({
              next:
                (response: any) => {
                  this.basicDetails = response;
                  this.msg.success(sharedConstant.BASIC_DETAIL_UPDATE);
                },
              error: () => this.msg.error(sharedConstant.BASIC_DETAIL_UPDATE_ERROR)
            })
          },
          error: () => this.msg.error(sharedConstant.BASIC_DETAIL_UPDATE_ERROR)
        });

      this.formBasicDetails.reset();
      this.isBasicDetailModalVisible = false;
    } else {
      Object.values(this.formBasicDetails.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleBasicDetailsModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formBasicDetails.reset();
    this.isBasicDetailModalVisible = false;
  }

  getStatesByCountry(value: any) {
    if (value) {
      this.formBasicDetails.controls.state.reset();
      this.formBasicDetails.controls.city.reset();
      this.selectedCountry = this.countries.find(
        (country) => country.name === value
      );
      this.states = State.getStatesOfCountry(this.selectedCountry.isoCode);
    }
  }
  onContryChange(value:any){
    if(value)
      this.getStatesByCountry(value)
  }
  getCitiesByState(value: any) {
    if (value) {
      this.formBasicDetails.controls.city.reset();
      this.selectedState = this.states.find((state) => state.name === value);
      this.cities = City.getCitiesOfState(
        this.selectedCountry.isoCode,
        this.selectedState.isoCode
      );
    }
  }

  uploadAvatar = (files: any) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const isJpgOrPng = fileToUpload.type === sharedConstant.IMAGE_JPEG || fileToUpload.type === sharedConstant.IMAGE_PNG;

    if (!isJpgOrPng) {
      this.msg.error(sharedConstant.IMAGE_VALIDATION_MESSAGE);
    } else {
      this.basicDetailService
        .updateAvatar(fileToUpload, this.userId)
        .subscribe((response: any) =>
          this.basicDetailService
            .getUserBasicDetails(this.userId)
            .subscribe((response: any) => {
              this.basicDetails = response;
              this.profileImage = false;
              this.msg.success(sharedConstant.IMAGE_UPDATED)
            })
        );
    }
  };

  handleResetPassword(): void {
    if (this.validateForm.valid) {
      const newPassword = this.validateForm.get(AdminDocumentConstants.PASSWORD)?.value;
      if (newPassword) {
        this.authService.changePassword(this.userId, newPassword).subscribe({
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

  openResetModal() {
    this.isResetModalVisible = true;
  }

  handleCancel(): void {
    this.validateForm.reset();
    this.isResetModalVisible = false;
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

  updateConfirmValidator(): void {
    Promise.resolve().then(() =>
      this.validateForm.controls.checkPassword.updateValueAndValidity()
    );
  }

  showProfileImageModel(): void {
    this.profileImage = true;
  }

  DeleteProfileImage(profilePhotoName: string): void {
    this.basicDetailService.deleteAvtar(this.userId).subscribe({
      next:
        (response: any) => {
          this.basicDetailService.getUserBasicDetails(this.userId).subscribe({
            next: (response: any) => {
              this.basicDetails = response;
              this.profileImage = false;
              this.msg.success(sharedConstant.IMAGE_DELETED)
            }
          })
        }
    });
  }

  onCancel(): void {
    this.profileImage = false;
  }

  onBack(): void {
    if (this.canGoBack)
      this.location.back();
    else
      this.router.navigate([AdminRoutesConstants.DASHBOARD, AdminRoutesConstants.MANAGE_USERS]);
  }
}
