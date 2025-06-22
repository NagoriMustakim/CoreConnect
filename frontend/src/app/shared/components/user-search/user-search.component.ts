import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NzListModule } from 'ng-zorro-antd/list';
import { CommonModule } from '@angular/common';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule, NzFormTooltipIcon } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageService } from 'ng-zorro-antd/message';
import { UserSearchService } from '../../services/user-search.service';
import { CustomValidators } from '../../helpers/CustomValidators';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../../environments/environment';

const count = 5;
const fakeDataUrl =
  'https://randomuser.me/api/?results=5&inc=name,gender,email,nat&noinfo';

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
  ],
  templateUrl: './user-search.component.html',
  styleUrl: './user-search.component.css',
})
export class UserSearchComponent implements OnInit {
  initLoading = true;
  loadingMore = false;
  isFilterModalVisible = false;
  isVertical = false;
  keywords = {};
  users: any;
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];

  validateForm!: FormGroup;
  captchaTooltipIcon: NzFormTooltipIcon = {
    type: 'info-circle',
    theme: 'twotone',
  };

  constructor(
    private http: HttpClient,
    private msg: NzMessageService,
    private fb: FormBuilder,
    private searchService: UserSearchService,
    private route: ActivatedRoute
  ) {
    const { maxLength, pattern } = CustomValidators;

    this.validateForm = this.fb.group({
      // keywords: ['', [maxLength(200), pattern('^[a-zA-Z\\s]*$')]],
      designation: ['', [pattern('^[a-zA-Z\\s\\.-]*$')]],
      experience: ['', [pattern('^[0-9]{0,60}$')]],
      skills: ['', [maxLength(100), pattern('^[^<>`,~\\s]*$')]],
      projects: ['', [maxLength(1000), pattern('^[a-zA-Z\\s]*$')]],
      certificates: ['', [maxLength(1000), pattern('^[a-zA-Z\\s]*$')]],
      training: ['', [maxLength(1000), pattern('^[a-zA-Z\\s]*$')]],
    });
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: 'Input is required',
    },
    default: {
      email: 'The input is not valid email',
    },
  };

  ngOnInit(): void {
    this.route.queryParams.subscribe((param: any) => {
      this.keywords = param.keywords;
      // this.getData(this.filterOptions);
      this.searchService.getUserList(this.keywords, this.validateForm.value)
        .subscribe((response: any) => {
          console.log(response);
          this.users = response;
          this.initLoading = false;
        });
    });
  }

  // getData(filterOptions:any): void {
  //   this.http.get('http://10.0.2.136:5289/api/user/search', {
  //     params: {keywords:filterOptions.keywords},
  //   })
  //     .pipe(catchError(() => of({ results: [] })))
  //     .subscribe((response: any) => {
  //       console.log(response);
  //       this.users = response;
  //       this.initLoading = false;
  //     });
  // }

  onLoadMore(): void {
    this.loadingMore = true;
    this.list = this.data.concat(
      [...Array(count)].fill({}).map(() => ({ loading: true, name: {} }))
    );
    this.http
      .get(environment.apiUrl + environment.roles.user + environment.routes.search)
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((response: any) => {
        this.data = this.data.concat(response.results);
        this.list = [...this.data];
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
        .getUserList(this.keywords, this.validateForm.value)
        .subscribe((response) => {
          console.log(response);
          this.users = response;
        });
      console.log('submit', this.keywords, this.validateForm.value);
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

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }
}
