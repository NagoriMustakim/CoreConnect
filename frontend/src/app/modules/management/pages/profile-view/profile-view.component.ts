import { IINternalProgram } from './../../interfaces/IInternalProgram';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzListModule } from 'ng-zorro-antd/list';
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
import { PronounService } from './../../../admin/service/pronoun.service';
import { BusinessUnitService } from './../../../admin/service/business-unit.service';
import { concatMap } from 'rxjs';
import { IBasicDetail } from './../../../candidate/interfaces/IBasicDetail';
import { BasicDetailService } from '../../services/basic-detail.service';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import {
  NonNullableFormBuilder,
  FormGroup,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { SkillComponent } from '../../components/skill/skill.component';
import { TrainingComponent } from '../../components/training/training.component';
import { ProjectComponent } from '../../components/project/project.component';
import { CertificationComponent } from '../../components/certification/certification.component';
import { CommonModule, Location } from '@angular/common';
import { ExperienceComponent } from '../../components/experience/experience.component';
import { KraComponent } from './../../components/kra/kra.component';
import { LanguageComponent } from './../../components/language/language.component';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzCardModule } from 'ng-zorro-antd/card';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
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
import { formatDistance } from 'date-fns';
import { NoiminationService } from '../../services/noimination.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { InternalprogramService } from '../../services/internalprogram.service';
import { CommentComponent } from '../../components/comment/comment.component';
import { NzResultModule } from 'ng-zorro-antd/result';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { ManagementRoutesConstants } from '../../constants/ManagementRoutesConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { environment } from '../../../../../environments/environment';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { InternalProgramCategoriesService } from '../../../admin/service/internal-program-categories.service';
import { NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';

@Component({
  selector: 'app-profile-view',
  standalone: true,
  imports: [
    NzCardModule,
    NzFlexModule,
    NzGridModule,
    NzCardModule,
    NzTabsModule,
    LanguageComponent,
    KraComponent,
    NzListModule,
    NzCommentModule,
    ExperienceComponent,
    CertificationComponent,
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
    CommentComponent,
    NzResultModule,
    NzTypographyModule,
    NzEmptyModule,
    NzPageHeaderModule,
    NzUploadModule
  ],
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css', '/src/styles.css'],
})
export class ProfileViewComponent implements OnInit, OnChanges {
  @Input() userId: string = '';
  userChildId: string = '';
  user!: any;
  isBasicDetailModalVisible = false;
  basicDetails = {} as any;
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
  saveMsg: string = ManagementDocumentConstants.ADD_MSG;
  isNominationModalVisible = false;
  isExportResumeModalVisible = false;
  internalPrograms: IINternalProgram[] = [];
  roles = ['RMG', 'HR', 'Manager'];
  currentUserRole: string = '';
  notFound = false;
  imageUrl = environment.imageUrl;
  canGoBack: boolean = false;
  internalProgramCategories: any;
  isInternalProgramCategoryExists: boolean | undefined;
  fileList: NzUploadFile[] = [];

  formNomination: FormGroup<{
    internalProgramGuid: FormControl<string>;
    internalProgramCategoryGuid: FormControl<string>;
    justificationComment: FormControl<string>;
    file: FormControl<string>;

  }>;

  formExportResume: FormGroup<{
    businessUnit: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private nominationService: NoiminationService,
    private msg: NzMessageService,
    private basicDetailService: BasicDetailService,
    private businessUnitService: BusinessUnitService,
    private pronounService: PronounService,
    private internalprogramservice: InternalprogramService,
    private location: Location,
    private eventEmitterService: EventEmitterService,
    private internalProgramCategoriesService: InternalProgramCategoriesService
  ) {
    this.canGoBack = !!(this.router.getCurrentNavigation()?.previousNavigation);

    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formBasicDetails = this.fb.group({
      id: [''],
      firstName: ['', [required, minLength(2), maxLength(50)]],
      lastName: ['', [required, minLength(2), maxLength(50)]],
      pronounGuid: [''],
      businessUnitGuid: ['', [required]],
      country: [''],
      state: [''],
      city: [''],
      designation: [''],
      birthDate: [''],
      about: [''],
    });

    this.formExportResume = this.fb.group({
      businessUnit: ['', [required]],
    });

    this.formNomination = this.fb.group({
      internalProgramGuid: ['', [required]],
      internalProgramCategoryGuid: [''],
      justificationComment: ['', [required, maxLength(500), pattern("[^<>`]*")]],
      file: [''],
    });

    let id = this.route.snapshot.paramMap.get(ManagementDocumentConstants.ID);
    if (window !== undefined && id !== null) {
      this.userId = id;
      window.localStorage.setItem(ManagementDocumentConstants.USERID, id);
    }

    if (this.userId) {
      this.getData(this.userId);
    }
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = params.get(ManagementDocumentConstants.ID)!;
    })

    this.countries = Country.getAllCountries();
  }

  ngOnChanges(): void {
    this.userChildId = this.userId;
    console.log("pv", this.userChildId);

    this.getData(this.userChildId);
  }

  formBasicDetails: FormGroup<{
    id: FormControl<string>;
    firstName: FormControl<string>;
    lastName: FormControl<string>;
    pronounGuid: FormControl<string>;
    businessUnitGuid: FormControl<string>;
    country: FormControl<string>;
    state: FormControl<string>;
    city: FormControl<string>;
    designation: FormControl<string>;
    birthDate: FormControl<string>;
    about: FormControl<string>;
  }>;

  avtarFallback = ManagementDocumentConstants.FALLBACK_AVATAR;
  businessUnitFallback = ManagementDocumentConstants.FALLBACK_BU;
  bannerFallback = ManagementDocumentConstants.FALLBACK_BANNER;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getData(userId: string) {
    this.pronounService
      .getPronouns()
      .pipe(
        concatMap((response: any) => {
          this.pronouns = response;

          return this.internalProgramCategoriesService.GetAllInternalProgramCategory(0, 0);
        })
      ).pipe(concatMap((response: any) => {
        this.internalProgramCategories = response.list;

        return this.businessUnitService.getBusinessUnits();
      }))
      .pipe(
        concatMap((response: any) => {
          this.businessUnits = response;

          return this.basicDetailService.getUBasicDetails(userId);
        })
      )
      .subscribe({
        next: (response: any) => {
          this.basicDetails = response;
          this.currentUserRole = this.basicDetails.role[0];
          this.eventEmitterService.callGetDetails();
        },
        error: (err) => {
          if (err.status == 404) this.notFound = true;
        },
      });
  }

  showNominationModal(msg: string): void {
    this.internalprogramservice
      .getInternalPrograms()
      .subscribe((response: any) => {
        this.internalPrograms = response.result;
        this.internalPrograms=this.internalPrograms.filter((internaProgram)=>internaProgram.isActive == true);
        console.log(this.internalPrograms);
      });
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

  createMessage(type: string, mes: string): void {
    this.msg.create(type, mes);
  }

  isRoleInRoles(userRole: string) {
    return this.roles.some((role) => role === userRole);
  }

  handleBasicDetailsModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formBasicDetails.reset();
    this.isBasicDetailModalVisible = false;
  }

  handleNominationModalOk(): void {
    if (this.formNomination.valid) {
      this.nominationService
        .addNomination(this.formNomination.value, this.userChildId, this.fileList)
        .subscribe({
          next: (response: any) => {
            this.msg.success(ManagementDocumentConstants.NOMINATED_MSG);
          },
          error: (err: any) => {
            if (err.status === 409) {
              this.msg.error(err.error.message)
            } else if (err.status === 500) {
              console.log(err);
              this.msg.error(err.error);
            } else {
              this.msg.error(ManagementDocumentConstants.NOMINATED_ERROR);
            }
          },
        });
      this.formNomination.reset();
      this.isNominationModalVisible = false;
      this.isInternalProgramCategoryExists = false;
      this.formNomination.controls.internalProgramCategoryGuid.disable();
    } else {
      Object.values(this.formNomination.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleExportResumeModalOk(): void {
    if (this.formExportResume.valid) {
      this.router.navigate(
        [ManagementRoutesConstants.MANAGEMENT_RESUME, this.formExportResume.value.businessUnit],
        { queryParams: { userId: this.userChildId } }
      );
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

  handleSubmit(): void {
    this.submitting = true;
    const content = this.inputValue;
    this.inputValue = '';
    setTimeout(() => {
      this.submitting = false;
      this.data = [
        ...this.data,
        {
          ...this.user,
          content,
          datetime: new Date(),
          displayTime: formatDistance(new Date(), new Date()),
        },
      ].map((e) => ({
        ...e,
        displayTime: formatDistance(new Date(), e.datetime),
      }));
    }, 800);
  }

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

  getStatesByCountry(value: any) {
    if (value) {
      this.selectedCountry = this.countries.find(
        (country) => country.name === value
      );
      this.states = State.getStatesOfCountry(this.selectedCountry.isoCode);
    }
  }

  getCitiesByState(value: any) {
    if (value) {
      this.selectedState = this.states.find((state) => state.name === value);
      this.cities = City.getCitiesOfState(
        this.selectedCountry.isoCode,
        this.selectedState.isoCode
      );
    }
  }

  onChange(value: any) {
    console.log(value);
    if (value) {
      const internalProgram = this.internalPrograms.find(ip => ip.internalProgramGuid == value);
      this.isInternalProgramCategoryExists = internalProgram?.isInternalProgramCategoryExists;

      if (this.isInternalProgramCategoryExists) {
        this.internalProgramCategoriesService.GetCategoriesByInternalProgramId(value).subscribe({
          next: (response) => {
            this.internalProgramCategories = response;
            console.log(this.internalProgramCategories);
          },
          error: (error) => {
            console.log(error);
          }
        });

        this.formNomination.controls.internalProgramCategoryGuid.enable();
        this.formNomination.controls.internalProgramCategoryGuid.setValidators(
          Validators.required
        );
        this.formNomination.controls.internalProgramCategoryGuid.markAsDirty();
      } else {
        this.formNomination.controls.internalProgramCategoryGuid.disable();
      }
    }
  }

  onBack(): void {
    if (this.canGoBack)
      this.location.back();
    else
      this.router.navigate([ManagementRoutesConstants.MANAGEMENT_SEARCH], { queryParams: { currentCount: 0, keywords: '' } });
  }

  beforeUpload = (file: NzUploadFile): boolean => {
    this.fileList = this.fileList.concat(file);
    this.fileList = this.fileList;
    return false;
  }
}
