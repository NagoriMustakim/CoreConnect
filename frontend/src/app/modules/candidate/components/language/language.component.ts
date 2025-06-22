import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { IProficieny } from './../../interfaces/proficiency';
import { ProficiencyService } from './../../../admin/service/proficiency.service';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { Language } from './../../../candidate/interfaces/Language';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { CommonModule } from '@angular/common';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import {
  FormsModule,
  FormGroup,
  FormControl,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { Component, Input, OnInit } from '@angular/core';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { LanguageService } from '../../services/language.service';
import { LanguageListService } from '../../../../shared/services/language-list.service';
import { concatMap } from 'rxjs';

@Component({
  selector: 'app-language',
  standalone: true,
  imports: [
    NzButtonModule,
    NzTabsModule,
    ReactiveFormsModule,
    NzInputModule,
    NzListModule,
    FormsModule,
    NzModalModule,
    NzCommentModule,
    NzSelectModule,
    CommonModule,
    NzCardModule,
    NzLayoutModule,
    NzFormModule,
    NzFlexModule,
    NzGridModule,
    NzIconModule,
    NzEmptyModule
  ],
  templateUrl: './language.component.html',
  styleUrls: ['./language.component.css', '/src/styles.css'],
})
export class LanguageComponent implements OnInit {
  isLanguageModalVisible = false;
  saveMsg: string = DocumentConstants.ADD_MSG;
  language: Language[] = [];
  proficieny: IProficieny[] = [];
  languages: string[] = [];
  initLoading = true;
  loadingMore = false;
  data: any[] = [];
  list: Array<{ loading: boolean; name: any }> = [];

  @Input() userId: string = '';
  constructor(
    private fb: NonNullableFormBuilder,
    private message: NzMessageService,
    private modal: NzModalService,
    private languageService: LanguageService,
    private proficiencyService: ProficiencyService,
    private languageListService: LanguageListService
  ) {
    this.languages = this.languageListService.getLanguageNames();
    const { required, maxLength, minLength } = CustomValidators;
    this.formLanguage = this.fb.group({
      languageGuid: [''],
      languageName: ['', [required, minLength(2), maxLength(50)]],
      proficiencyGuid: [''],
    });
  }

  formLanguage: FormGroup<{
    languageGuid: FormControl<string>;
    languageName: FormControl<string>;
    proficiencyGuid: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit(): void {
    this.getUserLanguages();
  }

  getUserLanguages(): void {
    this.languageService
    .getLanguages(this.userId)
    .pipe(concatMap((response: any) => {
      this.language = response;

      return this.proficiencyService.getProficiencyType();
    }))
    .subscribe((res: any) => {
      this.proficieny = res;
    });
  }

  showLanguageModal(language?: any): void {
    if (language) {
      this.saveMsg = DocumentConstants.EDIT_MSG;
      this.formLanguage.patchValue(language);
    } else {
      this.saveMsg = DocumentConstants.ADD_MSG;
    }
    this.isLanguageModalVisible = true;
  }

  handleLanguageModalOk(): void {
    if (this.formLanguage.valid) {
      if (this.formLanguage.value.languageGuid === '') {
        this.languageService
          .addLanguage(this.formLanguage.value)
          .subscribe({
            next:
              (response: any) => {
                this.language.push(response);
                this.message.success(sharedConstant.LANGUAGE_ADD)
              },
            error: () => this.message.error(sharedConstant.LANGUAGE_ERROR_ADD)
          });
        this.formLanguage.reset();
        this.isLanguageModalVisible = false;
      } else {
        this.languageService
          .updateLanguage(this.formLanguage.value)
          .subscribe({
            next:
              (res: any) => {
                let index = this.language.findIndex(
                  (language) =>
                    this.formLanguage.value.languageGuid == language.languageGuid
                );

                this.language[index] = {
                  ...this.language[index],
                  ...res,
                };
                this.formLanguage.reset();
                this.isLanguageModalVisible = false;
                this.message.success(sharedConstant.LANGUAGE_UPDATE)
              },
            error: () => this.message.error(sharedConstant.LANGUAGE_ERROR_UPDATE)
          });
      }
    } else {
      Object.values(this.formLanguage.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleLanguageModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formLanguage.reset();
    this.isLanguageModalVisible = false;
  }

  showLanguageDeleteConfirm(languageGuid: string): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.LANGUAGE_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.languageService
          .deleteLanguage(languageGuid)
          .subscribe({
            next:
              () => {
                (this.language = this.language.filter(
                  (language) => language.languageGuid !== languageGuid
                ))
                this.message.success(sharedConstant.LANGUAGE_DELETE)
              },
            error: () => this.message.error(sharedConstant.LANGUAGE_ERROR_DELETE)
          });
      },
      nzCancelText: DocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }
}
