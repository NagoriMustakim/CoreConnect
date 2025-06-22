import { CommonModule } from '@angular/common';
import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzTableModule, NzTableQueryParams } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { SkillsService } from '../../service/skills.service';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { Form, FormControl, FormGroup, FormsModule, NonNullableFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { ValidationConstants } from '../../../candidate/constants/ValidationConstants';

@Component({
  selector: 'app-skills',
  standalone: true,
  imports: [
    NzListModule,
    CommonModule,
    NzEmptyModule,
    NzButtonModule,
    NzIconModule,
    NzFlexModule,
    NzModalModule,
    NzTableModule,
    NzPaginationModule,
    NzDividerModule,
    NzTagModule,
    NzTypographyModule,
    NzPageHeaderModule,
    NzCardModule,
    NzTabsModule,
    NzCommentModule,
    NzFormModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzSpaceModule,
    NzImageModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzUploadModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzInputNumberModule,
    NzGridModule,
  ],
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.css','/src/styles.css'],
})

export class SkillsComponent {
  pageSize = 10;
  total = 0;
  skills: any[] = [];
  isSkillModalVisible: boolean = false;
  modalTitle : string = AdminDocumentConstants.ADD_MSG;

  formSkill: FormGroup<{
    skillGuid: FormControl<string>;
    skillTitle: FormControl<string>;
    skillDescription: FormControl<string>;
  }>;

  constructor(
    private skillsService: SkillsService,
    private modal: NzModalService,
    private msg: NzMessageService,
    private fb: NonNullableFormBuilder)
    {
      const { pattern, required, maxLength } = CustomValidators
      this.formSkill = this.fb.group({
        skillGuid: [''],
        skillTitle: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_NO_BRACKETS_DIGITS)]],
        skillDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
      });
   }

  showSkillModal(skill?: any): void {
    if (skill) {
      this.modalTitle = AdminDocumentConstants.EDIT_MSG;
      this.formSkill.patchValue(skill);
    } else {
      this.modalTitle = AdminDocumentConstants.ADD_MSG;
    }
    this.isSkillModalVisible = true;
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  getAllSkills(pageNumber: number = 1, pageSize: number = 10){
    this.skillsService.GetAllSkills(pageNumber, pageSize)
    .subscribe((response: any) =>{
      this.skills = response.list;
      this.total = response.count;
    });
  }

  handleSkillModalOk(): void {
    if (this.formSkill.valid) {
      if (this.formSkill.value.skillGuid === '') {
        this.skillsService.AddSkill(this.formSkill.value).subscribe({
          next:
          (response: any) => {
            this.skills.unshift(response);
            this.isSkillModalVisible = false;
            this.getAllSkills();
            this.msg.success(AdminDocumentConstants.SKILL_CREATED_MSG);
            this.formSkill.reset();
          },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_SKILL_CREATED_MSG);
            this.formSkill.reset();
            this.isSkillModalVisible = false;
          }
        });
      }
      else{
        this.skillsService
        .UpdateSkill(this.formSkill.value)
        .subscribe({
          next:
            (response: any) => {
              let index = this.skills.findIndex(
                (skill) =>
                  this.formSkill.value.skillGuid ==
                skill.skillGuid
              );

              this.skills[index] = {
                ...this.skills[index],
                ...response,
              };

              this.formSkill.reset();
              this.isSkillModalVisible = false;

              this.msg.success(AdminDocumentConstants.SKILL_UPDATED_MSG);
            },
          error: (err: any) => {
            this.msg.error(AdminDocumentConstants.ERROR_SKILL_UPDATED_MSG);
          }
        });
        }
    }
    else {
      Object.values(this.formSkill.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleSkillModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formSkill.reset();
    this.isSkillModalVisible = false;
  }

  showSkillDeleteConfirm(skillId: string){
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.SKILL_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {

        this.skillsService.DeleteSkill(skillId).subscribe((response: any) => {
          this.msg.success(AdminDocumentConstants.SKILL_DELETED_MSG)
          this.getAllSkills();
        })
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  onQueryParamsChange(params: NzTableQueryParams): void {
    const { pageSize, pageIndex } = params;
    this.getAllSkills(pageIndex, pageSize);
  }
}
