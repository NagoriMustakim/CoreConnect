import { NzResultModule } from 'ng-zorro-antd/result';
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
import { BasicDetailService } from '../../services/basic-detail.service';
import { NzModalModule } from 'ng-zorro-antd/modal';
import {
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
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
  Country,
  ICity,
  ICountry,
  IState,
} from 'country-state-city';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { environment } from '../../../../../environments/environment';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { RoutesConstants } from '../../constants/RoutesConstants';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';

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
    NzResultModule,
    NzTypographyModule,
    NzEmptyModule,
    NzPageHeaderModule
  ],
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.css', '/src/styles.css'],
})
export class ProfileViewComponent implements OnInit, OnChanges {
  @Input() userId: string = '';
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
  saveMsg: string = DocumentConstants.ADD_MSG;
  isNominationModalVisible = false;
  isExportResumeModalVisible = false;
  currentUserRole = '';
  notFound = false;
  imageUrl = environment.imageUrl;
  canGoBack: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private basicDetailService: BasicDetailService,
    private businessUnitService: BusinessUnitService,
    private pronounService: PronounService,
    private router: Router,
    private location: Location,
    private eventEmitterService: EventEmitterService
  ) {
    this.canGoBack = !!(this.router.getCurrentNavigation()?.previousNavigation);

    let id = this.route.snapshot.paramMap.get(DocumentConstants.ID);
    if (window !== undefined && id !== null) {
      this.userId = id;
      window.localStorage.setItem(DocumentConstants.USERID, id);
    }

    if (this.userId)
      this.getData(this.userId);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = params.get(DocumentConstants.ID)!;
    })

    this.countries = Country.getAllCountries();
  }

  ngOnChanges(): void {
    this.getData(this.userId);
  }

  avtarFallback = DocumentConstants.FALLBACK_AVATAR;
  businessUnitFallback = DocumentConstants.FALLBACK_BU;
  bannerFallback = DocumentConstants.FALLBACK_BANNER;

  getData(userId: string) {
    this.pronounService
      .getPronouns()
      .pipe(
        concatMap((response: any) => {
          this.pronouns = response;

          return this.businessUnitService.getBusinessUnits();
        })
      )
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

  onBack(): void {
    if (this.canGoBack)
      this.location.back();
    else
      this.router.navigate([RoutesConstants.CANDIDATE_SEARCH], { queryParams: { currentCount: 0, keywords: '' } });
  }
}
