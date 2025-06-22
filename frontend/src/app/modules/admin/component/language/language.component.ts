import { LanguageListService } from './../../../../shared/services/language-list.service';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { IProficieny } from './../../interfaces/proficiency';
import { ProficiencyService } from './../../service/proficiency.service';
import { LanguageService } from './../../service/language.service';
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
import { concatMap } from 'rxjs';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';

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
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
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
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formLanguage = this.fb.group({
      languageGuid: [''],
      languageName: ['', [required, minLength(2), maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS)]],
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
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit(): void {
    this.languageService
      .GetUserLanguages(this.userId)
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
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formLanguage.patchValue(language);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }
    this.isLanguageModalVisible = true;
  }

  handleLanguageModalOk(): void {
    if (this.formLanguage.valid) {
      if (this.formLanguage.value.languageGuid === '') {
        this.languageService
          .AddUserLanguage(this.formLanguage.value, this.userId)
          .subscribe({
            next:
              (response: any) => {
                this.language.push(response);
                this.formLanguage.reset();
                this.isLanguageModalVisible = false;
                this.message.success(sharedConstant.LANGUAGE_ADD);
              },
            error: (err: any) => {
              if (err.status === 409) {
                this.message.error(sharedConstant.LANGUAGE_ALREADY_EXISTS);
              } else {
                this.message.error(sharedConstant.LANGUAGE_ERROR_ADD);
                this.formLanguage.reset();
                this.isLanguageModalVisible = false;
              }
            }
          });
      } else {
        this.languageService
          .UpdateUserLangauge(this.formLanguage.value, this.userId)
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
                this.message.success(sharedConstant.LANGUAGE_UPDATE);
              },
            error: (err: any) => {
              if (err.status === 409) {
                this.message.error(sharedConstant.LANGUAGE_ALREADY_EXISTS);
              } else {
                this.message.error(sharedConstant.LANGUAGE_ERROR_UPDATE);
                this.formLanguage.reset();
                this.isLanguageModalVisible = false;
              }
            }
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
      nzTitle: AdminDocumentConstants.LANGUAGE_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.languageService
          .DeleteUserLangauge(languageGuid, this.userId)
          .subscribe({
            next:
              () => {
                (this.language = this.language.filter(
                  (language) => language.languageGuid !== languageGuid
                ))
                this.message.success(sharedConstant.LANGUAGE_DELETE);
              },
            error: (err: any) => {
              this.message.error(sharedConstant.LANGUAGE_ERROR_DELETE);
            }
          });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }
}
