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
import { NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';
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
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { IComment } from '../../interfaces/IComment';
import { CommentService } from '../../service/comment.service';
import { formatDistance } from 'date-fns';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { environment } from '../../../../../environments/environment';

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
    NzTypographyModule,
  ],
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css', '/src/styles.css'],
})
export class CommentComponent implements OnInit {
  @Input() userId: string = '';
  comments: IComment[] = [];
  submitting = false;
  isEditing: boolean = false;
  imageUrl = environment.imageUrl;

  constructor(
    private fb: NonNullableFormBuilder,
    private commentService: CommentService,
    private modal: NzModalService
  ) {
    const { required, maxLength, pattern } = CustomValidators;
    this.formComment = this.fb.group({
      commentGuid: [''],
      userId: [''],
      comment: [
        '',
        [
          required,
          maxLength(500),
          pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS),
        ],
      ],
    });

    this.formComment.controls.comment.disable();
  }

  ngOnInit() {
    this.getComments();
  }

  getComments() {
    this.commentService.getComments(this.userId).subscribe((response) => {
      this.comments = response;
      console.log(this.comments);
    });
  }

  formComment: FormGroup<{
    commentGuid: FormControl<string>;
    userId: FormControl<string>;
    comment: FormControl<string>;
  }>;

  handleCommentEdit(comment: IComment) {
    this.isEditing = true;
    this.formComment.patchValue(comment);
    this.formComment.controls.comment.enable();
  }

  handleCommentAddEdit(): void {
    this.formComment.controls.userId.setValue(this.userId);

    if (this.formComment.valid) {
      this.submitting = true;
      if (this.formComment.value.commentGuid !== '') {
        this.commentService
          .updateComment(this.formComment.value)
          .subscribe((response: IComment) => {
            this.getComments();

            this.formComment.reset();
            this.submitting = false;
            this.isEditing = false;
            this.formComment.controls.comment.disable();
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
      nzTitle: AdminDocumentConstants.COMMENT_DELETE_CONFIRMATION_MSG,
      nzCentered: true,
      nzContent: AdminDocumentConstants.DELETE_WARNING_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () =>
        this.commentService
          .deleteComment(commentId)
          .subscribe((response) => this.getComments()),
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  formatDate(date: any) {
    return formatDistance(new Date(date), new Date());
  }
}
