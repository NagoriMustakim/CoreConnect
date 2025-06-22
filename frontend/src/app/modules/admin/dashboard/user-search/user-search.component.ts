import { AuthService } from './../../../auth/service/auth.service';
import { HttpClient } from '@angular/common/http';
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
import { NzMessageService } from 'ng-zorro-antd/message';
import { UserSearchService } from '../../../../shared/services/user-search.service';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { environment } from '../../../../../environments/environment';
import { NzResultModule } from 'ng-zorro-antd/result';
import { OnlyNumberDirective } from '../../../../shared/directives/only-number.directive';

const count = 5;

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
    NzResultModule,
    RouterModule,
    OnlyNumberDirective
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
  validateForm!: FormGroup;
  imageUrl = environment.imageUrl;
  total: number = 0;

  constructor(
    private http: HttpClient,
    private msg: NzMessageService,
    private fb: FormBuilder,
    private searchService: UserSearchService,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {
    const { maxLength, pattern } = CustomValidators;
    this.validateForm = this.fb.group({
      designation: ['', [pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      experience: [0, [maxLength(2), pattern(AdminValidationConstants.REGEX_PATTERN_DIGITS)]],
      skills: ['', [maxLength(100), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      projects: ['', [maxLength(1000), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      certificates: ['', [maxLength(1000), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      training: ['', [maxLength(1000), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
    });
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit(): void {
    this.currentUserRole = this.authService.getRole();

    this.route.queryParams.subscribe((param: any) => {
      this.keywords = param.keywords;
      this.getUsers(0, this.keywords, null, true);
    });
  }

  onLoadMore(): void {
    this.getUsers(this.list.length, this.keywords, this.validateForm.value, false);
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
      });
  }

  showFilterModal(): void {
    this.isFilterModalVisible = true;
  }

  handleFiltersModalCancel(e: MouseEvent): void {
    this.isFilterModalVisible = false;
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      this.searchService
        .getUserList(0, this.keywords, this.validateForm.value)
        .subscribe((response: any) => {
          if (response.length >= 1) {
            this.data = response.list
            this.list = [...this.data];
            this.loadingMore = true;
          }
          else {
            this.data = response.list
            this.list = [...this.data];
            this.loadingMore = false
          }
        });
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
}
