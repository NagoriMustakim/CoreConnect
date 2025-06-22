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
  ReactiveFormsModule,
} from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule } from 'ng-zorro-antd/modal';
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
import { ExperienceComponent } from '../../components/experience/experience.component';
import { CertificationComponent } from '../../components/certification/certification.component';
import { ProjectComponent } from '../../components/project/project.component';
import { TrainingComponent } from '../../components/training/training.component';
import { NzProgressModule } from 'ng-zorro-antd/progress';
import { BasicDetailService } from '../../services/basic-detail.service';
import { IBasicDetail } from '../../interfaces/IBasicDetail';
import { Router } from '@angular/router';
import { HomeService } from '../../services/home.service';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { RoutesConstants } from '../../constants/RoutesConstants';
import { concatMap } from 'rxjs';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';

@Component({
  selector: 'app-home',
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
    NzProgressModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css', '/src/styles.css']
})
export class HomeComponent implements OnInit {

  hours: number = new Date().getHours();
  greeting: string = (this.hours < 12) ? DocumentConstants.GOOD_MORNING_MSG : ((this.hours >= 12 && this.hours <= 18) ? DocumentConstants.GOOD_AFTERNOON_MSG : DocumentConstants.GOOD_EVENING_MSG);
  basicDetails = {} as IBasicDetail;
  profileStrengthCounts: any = {};

  constructor(private basicDetailService: BasicDetailService,
    private router: Router,
    private homeService: HomeService,
    private eventEmitterService: EventEmitterService
  ) {
    this.basicDetailService.getBasicDetails().pipe((concatMap((response) => {
      this.basicDetails = response;

      return this.homeService.getProfileStrength();
    }))).subscribe((response) => {
      this.profileStrengthCounts = response;
      this.eventEmitterService.callGetDetails();
    });
  }

  ngOnInit() {
  }

  goToProfile() {
    this.router.navigate([RoutesConstants.CANDIDATE_PROFILE]);
  }

  goToGift() {
    this.router.navigate(['candidate/gift-program'])
  }
}
