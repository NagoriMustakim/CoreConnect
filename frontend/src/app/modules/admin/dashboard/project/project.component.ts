import { MstProjectService } from './../../service/mst-project.service';
import { sharedConstant } from './../../../../shared/constants/sharedDocumentConstant';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  NonNullableFormBuilder,
  ReactiveFormsModule,
  FormControl,
  FormGroup,
} from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { Project } from '../../../../shared/interface/project';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';

@Component({
  selector: 'app-project',
  standalone: true,
  imports: [
    NzEmptyModule,
    NzTableModule,
    NzModalModule,
    ReactiveFormsModule,
    NzFormModule,
    NzInputModule,
    NzFlexModule,
    NzButtonModule,
    NzIconModule,
    NzTypographyModule,
    NzSpaceModule,
    CommonModule,
  ],
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
})
export class ProjectComponent implements OnInit {
  projects: Project[] = [];
  saveMsg: string = AdminDocumentConstants.ADD_MSG;
  isProjectModal: boolean = false;
  total: number = 0;
  pageSize: number = 10;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  formProject: FormGroup<{
    projectGuid: FormControl<string>;
    projectTitle: FormControl<string>;
    projectDescription: FormControl<string>;
  }>;

  constructor(
    private fb: NonNullableFormBuilder,
    private modal: NzModalService,
    private message: NzMessageService,
    private projectService: MstProjectService
  ) {
    const { required, maxLength, minLength, pattern } = CustomValidators;
    this.formProject = this.fb.group({
      projectGuid: [''],
      projectTitle: [
        '',
        [
          required,
          minLength(2),
          maxLength(100),
          pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS),
        ],
      ],
      projectDescription: [
        '',
        [
          maxLength(500),
          pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS),
        ],
      ],
    });
  }

  ngOnInit() {
    this.getProjects();
  }

  getProjects(pageNumber: number = 1, pageSize: number = 10) {
    this.projectService.getProjects(pageNumber, pageSize).subscribe({
      next: (res: any) => {
        this.projects = res.list;
        this.total = res.count;
      },
    });
  }

  handleProjectModalOk(): void {
    if (this.formProject.valid) {
      if (this.formProject.value.projectGuid === '') {
        this.projectService.addProject(this.formProject.value).subscribe({
          next: (response: Project) => {
            this.projects.unshift(response);
            this.isProjectModal = false;
            this.getProjects();
            this.formProject.reset();
            this.message.success(sharedConstant.PROJECT_ADD);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.PROJECT_ERROR_ADD);
            this.formProject.reset();
            this.isProjectModal = false;
          },
        });
      } else {
        this.projectService.updateProject(this.formProject.value).subscribe({
          next: (response: Project) => {
            let index = this.projects.findIndex(
              (project) =>
                this.formProject.value.projectGuid == project.projectGuid
            );

            this.projects[index] = {
              ...this.projects[index],
              ...response,
            };

            this.formProject.reset();
            this.isProjectModal = false;
            this.message.success(sharedConstant.PROJECT_UPDATE);
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

  showProjectModal(project?: Project) {
    if (project) {
      this.saveMsg = AdminDocumentConstants.EDIT_MSG;
      this.formProject.patchValue(project);
    } else {
      this.saveMsg = AdminDocumentConstants.ADD_MSG;
    }

    this.isProjectModal = true;
  }
  handleProjectModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formProject.reset();
    this.isProjectModal = false;
  }
  showProjectDeleteConfirm(projectId: string) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.PROJECT_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.projectService.deleteProject(projectId).subscribe({
          next: (response: any) => {
            this.projects = this.projects.filter(
              (project) => project.projectGuid !== projectId
            );
            this.getProjects();
            this.message.success(sharedConstant.PROJECT_DELETE);
          },
          error: (err: any) => {
            this.message.error(sharedConstant.PROJECT_ERROR_DELETE);
          },
        }),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => {},
    });
  }
}
