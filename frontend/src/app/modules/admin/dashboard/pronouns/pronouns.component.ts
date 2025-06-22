import { Pronoun } from './../../interfaces/pronoun';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CommonModule } from '@angular/common';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzInputModule } from 'ng-zorro-antd/input';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { Component, OnInit } from '@angular/core';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { catchError, of } from 'rxjs';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { PronounService } from '../../service/pronoun.service';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';

@Component({
  selector: 'app-pronouns',
  standalone: true,
  imports: [
    NzFlexModule,
    NzEmptyModule,
    NzListModule,
    NzButtonModule,
    NzFormModule,
    CommonModule,
    NzModalModule,
    ReactiveFormsModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule
  ],
  templateUrl: './pronouns.component.html',
  styleUrls: ['./pronouns.component.css', '/src/styles.css'],
})
export class PronounsComponent implements OnInit {
  isModelVisible = false;
  isEditModalVisible = false;
  pronouns: Pronoun[] = [];

  constructor(
    public service: PronounService,
    private message: NzMessageService,
    private modal: NzModalService
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getData() {
    this.service
      .getPronouns()
      .pipe(catchError(() => of({ results: [] })))
      .subscribe((res: any) => {
        this.pronouns = res;
      });
  }

  showModal() {
    this.isModelVisible = true;
  }

  edit(item: any) {
    this.service.populateEditForm(item);
    this.isEditModalVisible = true;
  }

  delete(id: any) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.PRONOUN_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deletePronoun(id).subscribe((res: any) => {
          this.pronouns = this.pronouns.filter((item) => item.pronounGuid !== id);
          this.getData();
          this.message.success(AdminDocumentConstants.PRONOUN_DELETED_MSG)
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { }
    });
  }

  onSubmit() {
    if (this.service.createPronounModel.invalid) {
      Object.values(this.service.createPronounModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    } else {
      this.service.createPronoun().subscribe({
        next: (res) => {
          this.getData();
          this.message.success(AdminDocumentConstants.PRONOUN_CREATED_MSG);
          this.service.createPronounModel.reset();
          this.isModelVisible = false;
        },
        error: (err) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.PRONOUN_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_PRONOUN_CREATED_MSG);
            this.isModelVisible = false;
            this.service.createPronounModel.reset();
          }
        },
      });
    }
  }

  handlePronounEdit() {
    if (this.service.editPronounModel.valid) {
      this.service.updatePronoun().subscribe({
        next: () => {
          this.message.success(AdminDocumentConstants.PRONOUN_UPDATED_MSG);
          this.getData();
          this.handleCancel();
        },
        error: (err: any) => {
          if (err.status === 409) {
            this.message.error(AdminDocumentConstants.PRONOUN_EXISTS_MSG);
          } else {
            this.message.error(AdminDocumentConstants.ERROR_PRONOUN_UPDATED_MSG);
          }
        },
      });
    } else {
      Object.values(this.service.editPronounModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleCancel() {
    this.isModelVisible = false;
    this.isEditModalVisible = false;
  }
}
