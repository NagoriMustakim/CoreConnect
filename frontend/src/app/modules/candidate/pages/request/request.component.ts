import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzListModule } from 'ng-zorro-antd/list';
import { RequestService } from './../../services/request.service';
import { CustomValidators } from './../../../../shared/helpers/CustomValidators';
import { Component } from '@angular/core';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzLayoutModule } from 'ng-zorro-antd/layout';

import {
  FormControl,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  FormsModule
} from '@angular/forms';
import { Request } from '../../interfaces/Request';
import { ValidationConstants } from '../../constants/ValidationConstants';
import { DocumentConstants } from '../../constants/DocumentConstants';
@Component({
  selector: 'app-request',
  standalone: true,
  imports: [NzButtonModule, NzModalModule, NzInputModule, CommonModule, NzGridModule, NzLayoutModule, NzDropDownModule, NzFormModule, ReactiveFormsModule, NzListModule, NzSelectModule, FormsModule, NzFormModule, NzFlexModule, NzSkeletonModule],
  templateUrl: './request.component.html',
  styleUrl: './request.component.css'
})
export class RequestComponent {

  isRequestVisible = false;
  myRequests: Request[] = [];
  initLoading = true;
  constructor(private fb: NonNullableFormBuilder, private msg: NzMessageService, private requestService: RequestService) {
    const { required, maxLength, minLength } = CustomValidators

    this.formRequest = this.fb.group({
      type: ['', [required]],
      RequestDescription: ['', [required, minLength(2), maxLength(300)]],
      noOfMembers: ['', required,]
    });
  }

  formRequest: FormGroup<{
    type: FormControl<string>;
    RequestDescription: FormControl<string>;
    noOfMembers: FormControl<string>;
  }>;

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: ValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };

  ngOnInit() {
    this.getMyRequests();
  }

  getMyRequests(): void {
    this.requestService.UserRequests().subscribe((response: Request[]) => {
      this.myRequests = response;
    })
  }

  showModal(): void {
    this.isRequestVisible = true;
  }


  handleTrainingModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.formRequest.reset();
    this.isRequestVisible = false;
  }

  handleTrainingModalOk(): void {
    if (this.formRequest.valid) {
      this.requestService.addRequest(this.formRequest.value).subscribe({
        next: (response: any) => {
          console.log(response);

          this.myRequests.push(response)
          this.msg.success(DocumentConstants.REQUEST_CREATED_MSG)
        },
        error: (err: any) => {
          console.log(err);

          this.msg.error(err.error);
        }
      })

      this.formRequest.reset();
      this.isRequestVisible = false;
    }
    else {
      Object.values(this.formRequest.controls).forEach((control: any) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
}
