import { concatMap } from 'rxjs';
import { SkillListService } from './../../../../shared/services/skill-list.service';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { SkillService } from './../../services/skill.service';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzGridModule } from 'ng-zorro-antd/grid';
import {
  ReactiveFormsModule,
  NonNullableFormBuilder,
  FormGroup,
  FormControl,
} from '@angular/forms';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { Skill } from './../../../candidate/interfaces/Skill';
import { Component, Input } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { CommonModule } from '@angular/common';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { Proficiencies } from '../../../../shared/enums/proficiencies.enum';
@Component({
  selector: 'app-skill',
  standalone: true,
  imports: [
    NzButtonModule,
    NzCardModule,
    NzTabsModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    NzAvatarModule,
    NzInputModule,
    NzIconModule,
    NzFlexModule,
    NzSpaceModule,
    NzImageModule,
    NzModalModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    CommonModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzTagModule,
    NzInputModule,
    NzGridModule,
    NzEmptyModule,
  ],
  templateUrl: './skill.component.html',
  styleUrls: ['./skill.component.css', '/src/styles.css'],
})
export class SkillComponent {
  isSkillModalVisible = false;
  skills: Skill[] = [];
  skillsList: any[] = [];
  saveMsg: string = DocumentConstants.ADD_MSG;
  proficiencyKeys: any[];

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private skillService: SkillService,
    private message: NzMessageService,
    private skillListService: SkillListService
  ) {
    const { required } = CustomValidators;

    this.formSkill = this.fb.group({
      userSkillGuid: [''],
      skillGuid: ['', [required]],
      proficiencyId: ['',]
    });

    this.proficiencyKeys = Object.values(Proficiencies).filter(
      (value) => typeof value === 'number'
    );
  }

  formSkill: FormGroup<{
    userSkillGuid: FormControl<string>;
    skillGuid: FormControl<string>;
    proficiencyId: FormControl<string>;
  }>;

  @Input() userId: string = '';
  ngOnInit() {
    this.getData();
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  showSkillModal(skill?: Skill): void {
    if (skill) {
      this.saveMsg = DocumentConstants.EDIT_MSG;
      this.formSkill.patchValue(skill);
    } else {
      this.saveMsg = DocumentConstants.ADD_MSG;
    }

    this.isSkillModalVisible = true;
  }

  getData() {
    this.skillService
      .getSkills(this.userId)
      .pipe(
        concatMap((res: any) => {
          this.skills = res;
          console.log(res);

          return this.skillListService.GetAllSkills();
        })
      )
      .subscribe((res: any) => {
        this.skillsList = res.list;
      });
  }

  handleSkillModalOk(): void {
    if (this.formSkill.valid) {
      if (this.formSkill.value.userSkillGuid === '') {
        this.skillService.addSkill(this.formSkill.value).subscribe({
          next: (response: any) => {
            this.skills.push(response);
            this.formSkill.reset();
            this.isSkillModalVisible = false;
            this.message.success(sharedConstant.SKILL_ADD);
          },
          error: (err: any) => {
            if (err.status === 409) {
              this.message.error(sharedConstant.SKILL_ALREADY_EXISTS);
            } else {
              this.message.error(sharedConstant.SKILL_ERROR_ADD);
              this.formSkill.reset();
              this.isSkillModalVisible = false;
            }
          },
        });
      } else {
        this.skillService.updateSkill(this.formSkill.value).subscribe({
          next: () => {
            this.getData();
            let index = this.skills.findIndex(
              (skill) =>
                this.formSkill.value.userSkillGuid == skill.userSkillGuid
            );
            this.skills[index] = {
              ...this.skills[index],
              ...this.formSkill.value,
            };
            this.formSkill.reset();
            this.isSkillModalVisible = false;
            this.message.success(sharedConstant.SKILL_UPDATE);
          },
          error: (err: any) => {
            if (err.status === 409) {
              this.message.error(sharedConstant.SKILL_ALREADY_EXISTS);
            } else {
              this.message.error(sharedConstant.SKILL_ERROR_UPDATE);
              this.formSkill.reset();
              this.isSkillModalVisible = false;
            }
          },
        });
      }
    } else {
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

  showSkillDeleteConfirm(skillId: string): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.SKILL_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.skillService.deleteSkill(skillId).subscribe({
          next: () => {
            this.skills = this.skills.filter(
              (skill) => skill.userSkillGuid !== skillId
            );
            this.message.success(sharedConstant.SKILL_DELETE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.SKILL_ERROR_DELETE);
          },
        }),
      nzCancelText: DocumentConstants.NO_KEEP,
    });
  }

  getProficiencies(proficiency: Proficiencies): string {
    return Proficiencies[proficiency];
  }
}
