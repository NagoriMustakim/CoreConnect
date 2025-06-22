import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { KraService } from './../../../admin/service/kra.service';
import { Kra } from '../../interfaces/Kra';
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
import {
  FormsModule,
  ReactiveFormsModule,
  FormGroup,
  FormControl,
  NonNullableFormBuilder,
} from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-kra',
  standalone: true,
  imports: [
    NzFormModule,
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
  styleUrls: ['./kra.component.css', '/src/styles.css'],
})
export class KraComponent implements OnChanges {
  @Input() userId: string = '';
  loadingMore = false;
  initLoading = false;
  isKRAModalVisible = false;
  saveMsg: string = ManagementDocumentConstants.ADD_MSG;
  kra: Kra[] = [];
  list = [];
  startValue: Date | null = null;
  endValue: Date | null = null;

  constructor(private kraService: KraService, private fb: NonNullableFormBuilder, private modal: NzModalService) {
    const { required, maxLength, minLength, pattern } = CustomValidators;

    this.formKRA = this.fb.group({
      kraguid: [''],
      kratitle: ['', [required, minLength(2), maxLength(50), pattern(ManagementValidationConstants.REGEX_PATTERN_ALPHABETS)]],
      kradescription: ['', [maxLength(500), pattern(ManagementValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  ngOnChanges(): void {
    this.getkras();
  }

  formKRA: FormGroup<{
    kraguid: FormControl<string>;
    kratitle: FormControl<string>;
    kradescription: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ManagementValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getkras() {
    this.kraService.GetUserKras(this.userId).subscribe((response: any) => {
      this.kra = response;
      console.log(response);
    });
  }

  showKRAModal(kra?: Kra): void {
    if (kra) {
      this.saveMsg = ManagementDocumentConstants.EDIT_MSG;
      this.formKRA.patchValue(kra);
    } else {
      this.saveMsg = ManagementDocumentConstants.ADD_MSG;
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
      nzTitle: ManagementDocumentConstants.KRA_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: ManagementDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: ManagementDocumentConstants.YES_DELETE,
      nzOkType: ManagementDocumentConstants.PRIMARY_BUTTON_TYPE,
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
      nzCancelText: ManagementDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });

  }
}
