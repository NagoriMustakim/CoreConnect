import { concatMap } from 'rxjs';
import { ProjectsService } from './../../../../shared/services/projects.service';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { Component, Input, OnInit } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzCommentModule } from 'ng-zorro-antd/comment';
import { NzFormModule } from 'ng-zorro-antd/form';
import {
  FormControl,
  FormGroup,
  FormsModule,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { CommonModule } from '@angular/common';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { IProject } from '../../interfaces/IProject';
import { ProjectService } from '../../services/project.service';
import { DocumentConstants } from '../../constants/DocumentConstants';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { SkillListService } from '../../../../shared/services/skill-list.service';

@Component({
  selector: 'app-project',
  standalone: true,
  imports: [
    NzButtonModule,
    NzCardModule,
    NzTabsModule,
    NzListModule,
    NzCommentModule,
    NzFormModule,
    FormsModule,
    NzAvatarModule,
    NzInputModule,
    NzIconModule,
    NzFlexModule,
    NzSpaceModule,
    NzImageModule,
    NzModalModule,
    ReactiveFormsModule,
    NzDatePickerModule,
    NzUploadModule,
    CommonModule,
    NzTimelineModule,
    NzSelectModule,
    NzCheckboxModule,
    NzSkeletonModule,
    NzTagModule,
    NzInputNumberModule,
    NzGridModule,
    NzEmptyModule,
    NzTypographyModule,
  ],
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css', '/src/styles.css'],
})
export class ProjectComponent implements OnInit {
  @Input() userId: string = '';
  projects: IProject[] = [];
  projectList: any[] = [];
  initLoading = true;
  isProjectModalVisible = false;
  saveMsg: string = DocumentConstants.ADD_MSG;
  startValue: Date | null = null;
  endValue: Date | null = null;
  skillsList : any[]=[]
  listOfSelectedSkills: string[] = [];

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private projectService: ProjectService,
    private projectListService: ProjectsService,
    private skillService : SkillListService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;

    this.formProject = this.fb.group({
      userProjectGuid: [''],
      projectGuid: ['', [required]],
      projectStartDate: ['', [required]],
      projectEndDate: ['', [required]],
      isProjectActive: [false, [required]],
      projectDescription: [
        '',
        [
          maxLength(500),
          pattern(ValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS),
        ],
      ],
      skillsGuids: [''],
    });
  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.projectListService
    .getProjects()
    .pipe(
      concatMap((response: any) => {
        this.projectList = response.list;
        return this.skillService.GetAllSkills();
      })
      ).pipe(concatMap((response:any)=>{
        this.skillsList = response.list
        return this.projectService.getProjects(this.userId);
    }))
    .subscribe((response) => {
      this.projects = response;
      this.initLoading = false;
    });
  }

  formProject: FormGroup;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  showProjectModal(project?: IProject): void {
    if (project) {
      this.saveMsg = DocumentConstants.EDIT_MSG;
      this.formProject.patchValue({
        userProjectGuid:project.userProjectGuid,
        projectGuid :project.projectGuid,
        projectStartDate : project.projectStartDate,
        projectEndDate : project.projectEndDate,
        isProjectActive : project.isProjectActive,
        projectDescription : project.projectDescription,
        skillsGuids : project.skills
      });
      this.listOfSelectedSkills = project.skills.map(skill=>skill.skillGuid)
      this.handleCurrentProjectCheckbox(project.isProjectActive);
      this.handleStartOpenChange();
      this.handleEndOpenChange();
      this.disabledStartDate(new Date(project!.projectStartDate));
      this.disabledEndDate(new Date(project!.projectEndDate));

    } else {
      this.saveMsg = DocumentConstants.ADD_MSG;
    }

    this.isProjectModalVisible = true;
  }

  handleProjectModalOk(): void {
    if (this.formProject.valid) {
      this.formProject.controls['skillsGuids'].setValue(this.listOfSelectedSkills);

      if (this.formProject.value.userProjectGuid === '') {
        this.projectService.addProject(this.formProject.value).subscribe({
          next: (response: IProject) => {
            this.getData();
            this.listOfSelectedSkills=[];
            this.formProject.reset();
            this.isProjectModalVisible = false;
            this.message.success(sharedConstant.PROJECT_ADD);
            this.startValue = null;
            this.endValue = null;
          },
          error: (err: any) => {
            this.message.error(sharedConstant.PROJECT_ERROR_ADD);
          },
        });
      } else {
        this.projectService.updateProject(this.formProject.value).subscribe({
          next: () => {
            this.getData();
            let index = this.projects.findIndex(
              (project) =>
                this.formProject.value.userProjectGuid ==
                project.userProjectGuid
            );
            this.projects[index] = {
              ...this.projects[index],
              ...this.formProject.value,
            };
            this.listOfSelectedSkills=[];
            this.formProject.reset();
            this.isProjectModalVisible = false;
            this.message.success(sharedConstant.PROJECT_UPDATE);
            this.startValue = null;
            this.endValue = null;
          },
          error: (err: any) => {
            this.message.error(sharedConstant.PROJECT_ERROR_UPDATE);
          },
        });
      }
    } else {
      Object.values(this.formProject.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  handleProjectModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.isProjectModalVisible = false;
  }

  handleCurrentProjectCheckbox(required: boolean): void {
    if (required) {
      this.formProject.controls['projectEndDate'].clearValidators();
      this.formProject.controls['projectEndDate'].markAsPristine();
    } else {
      this.formProject.controls['projectEndDate'].setValidators(
        Validators.required
      );
      this.formProject.controls['projectEndDate'].markAsDirty();
    }
    this.formProject.controls['projectEndDate'].updateValueAndValidity();
  }

  handleAfterProjectModalClose() {
    this.listOfSelectedSkills=[];
    this.formProject.reset();
    this.formProject.controls['projectEndDate'].setValidators(Validators.required);
  }

  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue || !this.endValue) {
      return false;
    }
    return startValue.getTime() > this.endValue.getTime();
  };

  disabledEndDate = (endValue: Date): boolean => {
    if (!endValue || !this.startValue) {
      return false;
    }
    return endValue.getTime() <= this.startValue.getTime();
  };

  handleStartOpenChange(): void {
    if (
      this.formProject.controls['projectStartDate'].value &&
      this.formProject.controls['projectStartDate'].value !== ''
    ) {
      this.startValue = new Date(
        this.formProject.controls['projectStartDate'].value
      );
    } else this.startValue = null;
  }

  handleEndOpenChange(): void {
    if (
      this.formProject.controls['projectEndDate'].value &&
      this.formProject.controls['projectEndDate'].value !== ''
    ) {
      this.endValue = new Date(this.formProject.controls['projectEndDate'].value);
    } else this.endValue = null;
  }

  showProjectDeleteConfirm(projectId: string): void {
    this.modal.confirm({
      nzTitle: DocumentConstants.PROJECT_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: DocumentConstants.DELETE_WARNING_MSG,
      nzOkText: DocumentConstants.YES_DELETE,
      nzOkType: DocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.projectService.deleteProject(projectId).subscribe({
          next: () => {
            this.projects = this.projects.filter(
              (project) => project.userProjectGuid !== projectId
            );
            this.message.success(sharedConstant.PROJECT_DELETE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.PROJECT_ERROR_DELETE);
          },
        }),
      nzCancelText: DocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }
  isNotSelected(value: string): boolean {
    return this.listOfSelectedSkills.indexOf(value) === -1;
  }
}
