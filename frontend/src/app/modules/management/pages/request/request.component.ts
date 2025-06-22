import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzListModule } from 'ng-zorro-antd/list';
import { RequestService } from './../../services/request.service';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzCardModule } from 'ng-zorro-antd/card';
import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  FormsModule
} from '@angular/forms';
import { Request } from '../../interfaces/Request';
import { Status } from '../../../../shared/enums/status.enum';
import { FoodRequest } from '../../../../shared/enums/food-request.enum';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { EventEmitterService } from '../../../../shared/services/event-emitter.service';
import {NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { UserSearchService } from '../../../../shared/services/user-search.service';
import { BehaviorSubject, catchError, debounceTime, map, Observable, of, switchMap, concatMap } from 'rxjs';

@Component({
  selector: 'app-request',
  standalone: true,
  imports: [NzButtonModule,
    NzTypographyModule,
    NzModalModule,
    NzInputModule,
    CommonModule,
    NzGridModule,
    NzLayoutModule,
    NzDropDownModule,
    NzFormModule,
    ReactiveFormsModule,
    NzListModule,
    NzSelectModule,
    FormsModule,
    NzFormModule,
    NzFlexModule,
    NzSkeletonModule,
    NzCardModule,
    NzTableModule,
    NzIconModule,
    NzTagModule,
    NzPaginationModule,
    NzSpaceModule,
    NzDatePickerModule,
    NzSelectModule
  ],
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css', '/src/styles.css']
})
export class RequestComponent {

  pageIndex = 1;
  pageSize = 10;
  total = 0;
  isRequestVisible = false;
  requests: Request[] = [];
  initLoading = true;
  statusList = [
    { text: Status[Status.Pending], value: 0, checked: true },
    { text: Status[Status.Approved], value: 1, checked: false },
    { text: Status[Status.Rejected], value: 2, checked: false }
  ];

  startValue: Date | null = null;
  endValue: Date | null = null;
  optionList: any[] = [];
  searchChange$ = new BehaviorSubject('');
  selectedUser?: string[];
  isLoading = false;
  keywords='';
  expandSet = new Set<string>();


  constructor(private fb: NonNullableFormBuilder,
    private msg: NzMessageService,
    private requestService: RequestService,
    private eventEmitterService: EventEmitterService,
    private userListService: UserSearchService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators

    this.formRequest = this.fb.group({
      type: ['', [required]],
      RequestDescription: ['', [required, minLength(2), maxLength(500), pattern(ManagementValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      noOfMembers: ['', required],
      requestDate: [''],
      users:['']
    });
  }

  formRequest: FormGroup<{
    type: FormControl<string>;
    RequestDescription: FormControl<string>;
    noOfMembers: FormControl<string>;
    requestDate: FormControl<string>;
    users:FormControl<any>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit() {
    this.getData([], this.pageIndex, this.pageSize);
  }
 onExpandChange(requestGuid: string, checked: boolean): void {
    if (checked) {
      this.expandSet.add(requestGuid);
    } else {
      this.expandSet.delete(requestGuid);
    }
  }
  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex, filter } = params;
    this.getData(filter, pageIndex, pageSize);
  }
  
  getData(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10){
    this.requestService.getRequests(filter, pageNumber, pageSize).subscribe((response: any) => {
      this.requests = response.list;
      this.total = response.totalCount;
      this.eventEmitterService.callGetDetails();

      const getRandomNameList = (keywords: string): Observable<any> =>
      this.userListService.getUserList(0,keywords)
        .pipe(
          catchError(() => of({ results: [] })),
          map((res: any) => res.list)
        )
        .pipe(map((list: any) => list));

    const optionList$: Observable<string[]> = this.searchChange$
      .asObservable()
      .pipe(debounceTime(500))
      .pipe(switchMap(getRandomNameList));

    optionList$.subscribe(data => {
      this.optionList = data;
      this.isLoading = false;
    });
    })
  }

  // getRequests(filter: Array<{ key: string; value: string[] }>, pageNumber: number = 1, pageSize: number = 10) {
  //   this.requestService.getRequests(filter, pageNumber, pageSize).subscribe((response: any) => {
  //     this.requests = response.list;
  //     this.total = response.totalCount;
  //     this.eventEmitterService.callGetDetails();
  //   })
  // }
  onSearch(value: string): void {
    this.isLoading = true;
    this.searchChange$.next(value);
  }
  showModal(): void {
    this.isRequestVisible = true;
  }

  getStatus(status: Status): string {
    return Status[status];
  }

  getFoodStatus(foodStatus: FoodRequest): string {
    return FoodRequest[foodStatus];
  }

  handleRequestModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formRequest.reset();
    this.isRequestVisible = false;
  }

  handleRequestModalOk(): void {

    this.formRequest.controls.users.setValue(this.selectedUser)
    if (this.formRequest.valid) {
      this.requestService.addRequest(this.formRequest.value).subscribe({
        next: (response: any) => {
          this.requests.unshift(response);
          this.msg.success(ManagementDocumentConstants.REQUEST_MSG);
        },
        error: (err: any) => {
          this.msg.error(err.error)
        }
      })

      this.selectedUser =[]
      this.optionList=[]
      this.formRequest.reset();
      this.isRequestVisible = false;
    }
    else {
      Object.values(this.formRequest.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue || !this.endValue) {
      return false;
    }
    return startValue.getTime() > this.endValue.getTime();
  };

  handleStartOpenChange(): void {
    if (this.formRequest.controls.requestDate.value && this.formRequest.controls.requestDate.value !== '') {
      this.startValue = new Date(this.formRequest.controls.requestDate.value);
    }
    else
      this.startValue = null;
  }
}
