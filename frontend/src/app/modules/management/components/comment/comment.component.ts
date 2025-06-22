import { environment } from './../../../../../environments/environment';
import { Component, Input, OnChanges } from '@angular/core';
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
import { CustomValidators } from '../../../../shared/helpers/CustomValidators';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { IComment } from '../../interfaces/IComment';
import { CommentService } from '../../services/comment.service';
import { formatDistance } from 'date-fns';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { AuthService } from '../../../auth/service/auth.service';
import { ManagementDocumentConstants } from '../../constants/ManagementDocumentConstants';
import { ManagementValidationConstants } from '../../constants/ManagementValidationConstants';

@Component({
  selector: 'app-comment',
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
    NzTypographyModule
  ],
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css', '/src/styles.css']
})
export class CommentComponent implements OnChanges {
  @Input() userId: string = '';
  comments: IComment[] = [];
  submitting: boolean = false;
  loggedInUserId: string = '';
  imageUrl = environment.imageUrl;

  constructor(private fb: NonNullableFormBuilder, private commentService: CommentService, private modal: NzModalService, private authService: AuthService) {
    this.loggedInUserId = authService.getUserId();
    const { required, maxLength, pattern } = CustomValidators;
    this.formComment = this.fb.group({
      commentGuid: [''],
      userId: [''],
      comment: ['', [required, maxLength(500), pattern(ManagementValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]]
    });
  }

  ngOnChanges() {
    this.getComments();
  }

  getComments() {
    this.comments = [];
    this.commentService.getComments(this.userId).subscribe((response) => {
      this.comments = response;
    });
  }

  formComment: FormGroup<{
    commentGuid: FormControl<string>;
    userId: FormControl<string>;
    comment: FormControl<string>;
  }>;

  handleCommentEdit(comment: IComment) {
    this.formComment.patchValue(comment);
  }

  handleCommentAddEdit(): void {
    this.formComment.controls['userId'].setValue(this.userId);

    if (this.formComment.valid) {
      this.submitting = true;
      if (this.formComment.value.commentGuid === '') {
        this.commentService
          .addComment(this.formComment.value)
          .subscribe((response: IComment) => {
            this.getComments();

            this.formComment.reset();
            this.submitting = false;
          });
      } else {
        this.commentService
          .updateComment(this.formComment.value)
          .subscribe((response: IComment) => {
            this.getComments();

            this.formComment.reset();
            this.submitting = false;
          });
      }
    } else {
      Object.values(this.formComment.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  showCommentDeleteConfirm(commentId: string): void {
    this.modal.confirm({
      nzTitle: ManagementDocumentConstants.COMMENT_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: ManagementDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: ManagementDocumentConstants.YES_DELETE,
      nzOkType: ManagementDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.commentService
          .deleteComment(commentId)
          .subscribe(response => this.getComments()),
      nzCancelText: ManagementDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }


  formatDate(date: any) {
    return formatDistance(new Date(date), new Date());
  }
}
