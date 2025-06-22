import { AuthService } from './../../../auth/service/auth.service';
import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NzListModule } from 'ng-zorro-antd/list';
import { CommonModule } from '@angular/common';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { UserSearchService } from '../../../../shared/services/user-search.service';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { environment } from '../../../../../environments/environment';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzResultModule } from 'ng-zorro-antd/result';
import { OnlyNumberDirective } from '../../../../shared/directives/only-number.directive';
import { ProfileViewComponent } from '../profile-view/profile-view.component';
import { CommentComponent } from '../../components/comment/comment.component';
import { NzAffixModule } from 'ng-zorro-antd/affix';

@Component({
  selector: 'app-user-search',
  standalone: true,
  imports: [
    NzIconModule,
    NzSkeletonModule,
    NzFlexModule,
    NzGridModule,
    NzListModule,
    NzCardModule,
    CommonModule,
    NzFormModule,
    NzUploadModule,
    NzModalModule,
    NzInputModule,
    NzButtonModule,
    ReactiveFormsModule,
    NzSelectModule,
    NzCheckboxModule,
    NzEmptyModule,
    NzResultModule,
    RouterModule,
    OnlyNumberDirective,
    ProfileViewComponent,
    CommentComponent,
    NzAffixModule
  ],
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css', '/src/styles.css'],
})
export class UserSearchComponent implements OnInit {
  initLoading = true;
  loadingMore = true;
  isFilterModalVisible = false;
  isVertical = false;
  keywords = {};
  users: any;
  data: any[] = [];
  list: Array<{ profilePhotoName: string; employeeName: string; designation: string; userId: string; loading: boolean }> = [];
  currentUserRole: string = '';
  currentLogedUserId: string = '';
  validateForm!: FormGroup;
  roles = ['RMG', 'HR', 'Manager'];
  imageUrl = environment.imageUrl;
  userId: string = '';
  total: number = 0;

  constructor(
    private fb: FormBuilder,
    private searchService: UserSearchService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private eventEmitterService: EventEmitterService

  ) {
    const { maxLength, pattern } = CustomValidators;
    this.validateForm = this.fb.group({
      designation: ['', [pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      experience: ['', [maxLength(2), pattern(ManagementValidationConstants.REGEX_PATTERN_DIGITS)]],
      skills: ['', [maxLength(100), pattern(ManagementValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      projects: ['', [maxLength(1000), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      certificates: ['', [maxLength(1000), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      training: ['', [maxLength(1000), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
    });
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    }
  };

  ngOnInit(): void {
    this.currentUserRole = this.authService.getRole();
    console.log(this.currentUserRole);

    this.currentLogedUserId = this.authService.getUserId();

    this.route.queryParams.subscribe((param: any) => {
      this.keywords = param.keywords;
      this.getUsers(0, this.keywords, null, true);
    });
  }

  getUsers(currentCount: number, keywords: any, data: any, inplace: boolean) {
    this.searchService.getUserList(currentCount, keywords, data)
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((response: any) => {
        this.list = inplace ? response.list : this.list.concat(response.list);

        this.total = response.count;
        this.initLoading = false;
        this.validateForm.reset();

        if (this.total > this.list.length)
          this.loadingMore = true;
        else
          this.loadingMore = false;

        this.eventEmitterService.callGetDetails();
      });
  }

  isRoleInRoles(userRole: string) {
    return this.roles.some(role => role === userRole);
  }

  onLoadMore(): void {
    this.getUsers(this.list.length, this.keywords, this.validateForm.value, false);
  }

  showFilterModal(): void {
    this.isFilterModalVisible = true;
  }

  handleFilterModalCancel(): void {
    this.isFilterModalVisible = false;
  }

  handleFilterReset() {
    this.validateForm.reset();
    this.getUsers(0, this.keywords, this.validateForm.value, true);
    this.isFilterModalVisible = false;
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      this.getUsers(0, this.keywords, this.validateForm.value, true);
      this.isFilterModalVisible = false;
    } else {
      Object.values(this.validateForm.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  getProfileById(userId: string) {
    console.log("search", userId);
    this.userId = userId;
  }
}