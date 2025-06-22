import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { KraService } from './../../service/kra.service';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { Kra } from './../../../candidate/interfaces/Kra';
import { Component, Input } from '@angular/core';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormControl, NonNullableFormBuilder } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { ManagementValidationConstants } from '../../../management/constants/ManagementValidationConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
@Component({
  selector: 'app-kra',
  standalone: true,
  imports: [NzFormModule,
    FormsModule,
    CommonModule,
    NzInputModule,
    NzInputNumberModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzGridModule,
    NzModalModule,
    NzIconModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    NzFlexModule,
    NzSelectModule,
    NzLayoutModule,
    NzModalModule,
    NzTimelineModule,
    NzTimePickerModule,
    NzButtonModule,
    NzSpaceModule,
    NzTagModule,
    NzSkeletonModule,
    NzCardModule,
    NzEmptyModule,
    NzTypographyModule
  ],
  templateUrl: './kra.component.html',
  styleUrls: ['./kra.component.css', '/src/styles.css']
})
export class KraComponent {

  loadingMore = false;
  initLoading = false;
  isKRAModalVisible = false;
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  kra: Kra[] = [];
  list = []
  startValue: Date | null = null;
  endValue: Date | null = null;

  @Input() userId: string = '';

  constructor(private fb: NonNullableFormBuilder, private modal: NzModalService, private kraService: KraService) {

    const { required, maxLength, minLength, min, pattern } = CustomValidators;

    this.formKRA = this.fb.group({
      kraguid: [''],
      kratitle: ['', [required, minLength(2), maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      kradescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  formKRA: FormGroup<{
    kraguid: FormControl<string>;
    kratitle: FormControl<string>;
    kradescription:FormControl<string>
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit() {
    this.getkras()
  }

  getkras() {
    this.kraService.GetUserKras(this.userId).subscribe((response: any) => {
      this.kra = response;
    })
    console.log(this.formKRA.value);
  }

  showKRAModal(kra?: Kra): void {
    if (kra) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formKRA.patchValue(kra);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }
    this.isKRAModalVisible = true;
  }



  handleKRAModalOk(): void {

    if (this.formKRA.valid) {
      if (this.formKRA.value.kraguid === '') {
        this.kraService.AddUserKra(this.formKRA.value, this.userId).subscribe((response: any) => {
          this.kra.push(response)
        })
        this.formKRA.reset();
        this.isKRAModalVisible = false;
      } else {
        this.kraService.UpdateUserKra(this.formKRA.value, this.userId).subscribe({
          next: (response: any) => {
            this.getkras()
            let index = this.kra.findIndex((kra) => this.formKRA.value.kraguid == kra.kraguid);

            this.kra[index] = {
              ...this.kra[index],
              ...this.formKRA.value
            };
            this.formKRA.reset();
            this.isKRAModalVisible = false;
          },
          error: (err: any) => {
            console.log(err);
            this.formKRA.reset();
            this.isKRAModalVisible = false;
          }
        })
      }

    } else {
      Object.values(this.formKRA.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleKRAModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formKRA.reset();
    this.isKRAModalVisible = false;
  }

  showKRADeleteConfirm(kraguid: string): void {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.KRA_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.kraService.DeleteUserKra(kraguid, this.userId).subscribe({
          next: (response: any) => {
            this.kra = this.kra.filter((kra) => kra.kraguid !== kraguid)
          },
          error: (err: any) => {
            console.log(err);
          }
        })
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });

  }
}
