import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ManageUserService } from '../../service/manage-user.service';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { Router } from '@angular/router';
import { NzTableModule } from 'ng-zorro-antd/table';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AdminDocumentConstants } from '../../constants/AdminDocumentConstants';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzInputModule } from 'ng-zorro-antd/input';
import { AuthService } from '../../../auth/service/auth.service';
import {
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { AdminValidationConstants } from '../../constants/AdminValidationConstants';
import * as Papa from 'papaparse';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { Roles } from '../../../../shared/enums/roles.enum';
import { DesginationService } from '../../../../shared/services/designation.service';

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [
    NzListModule,
    ReactiveFormsModule,
    NzIconModule,
    NzInputModule,
    NzButtonModule,
    NzEmptyModule,
    NzModalModule,
    NzTableModule,
    NzPaginationModule,
    NzFormModule,
    CommonModule,
    NzSelectModule,
    NzDatePickerModule,
    NzFlexModule,
    NzSpaceModule,
  ],
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.css', '/src/styles.css'],
})
export class ManageUsersComponent implements OnInit {
  csvUploadForm: FormGroup;
  userList: any[] = [];
  pageSize = 10;
  total = 0;
  isRegisterModalVisible: boolean = false;
  errors: any = [];
  visible = false;
  passwordVisible = false;
  confirmPasswordVisible = false;
  isRegistering = false;
  roleKeys: any[];
  designations: any;

  constructor(
    private cdr: ChangeDetectorRef,
    private fb: NonNullableFormBuilder,
    public service: ManageUserService,
    public authService: AuthService,
    private modal: NzModalService,
    private msg: NzMessageService,
    private router: Router,
    private designationService: DesginationService
  ) {
    this.csvUploadForm = this.fb.group({
      csvFile: [''],
    });

    this.roleKeys = Object.values(Roles).filter(
      (value) => typeof value === 'number'
    );
  }

  ngOnInit(): void {
    this.getData();
  }

  showRegisterModal() {
    this.designationService.getAllDesignation().subscribe((response: any) => {
      this.designations = response.list;
    });
    this.isRegisterModalVisible = true;
  }

  handleRegisterModalCancel(e: MouseEvent): void {
    e.preventDefault();
    this.authService.formModel.reset();
    this.isRegisterModalVisible = false;
  }

  getData(pageNumber: number = 1, pageSize: number = 10) {
    this.service.getAllUsers(pageNumber, pageSize).subscribe({
      next: (res: any) => {
        this.userList = res.list;
        this.total = res.count;
        this.cdr.detectChanges();
      },
      error: (err) => console.log(err),
    });
  }

  view(item: any) {
    this.router.navigateByUrl(
      AdminDocumentConstants.ROUTE_DASHBOARD_PROFILE + item.id
    );
  }

  delete(id: string) {
    this.modal.confirm({
      nzTitle: AdminDocumentConstants.USER_DELETE_CONFIRMATION_MSG,
      nzOkText: AdminDocumentConstants.YES_DELETE,
      nzOkType: AdminDocumentConstants.PRIMARY_BUTTON_TYPE,
      nzOkDanger: true,
      nzOnOk: () => {
        this.service.deleteUser(id).subscribe((res: any) => {
          this.getData();
          this.msg.success(AdminDocumentConstants.USER_DELETED_MSG);
        });
      },
      nzCancelText: AdminDocumentConstants.NO_KEEP,
      nzOnCancel: () => { },
    });
  }

  async handleRegisterModalOk(): Promise<void> {
    if (this.authService.formModel.valid) {
      try {
        this.isRegistering = true;
        const res = await this.authService.register().toPromise();
        this.isRegistering = false;
        this.msg.success(AdminDocumentConstants.USER_REGISTERED_MSG);
        this.authService.formModel.reset();
        this.isRegisterModalVisible = false;
      } catch (err: any) {
        this.isRegistering = false;
        this.errors.push(err.error[0].description);
        this.msg.error(
          AdminDocumentConstants.ERROR_USER_REGISTERED_MSG +
          err.error[0].description
        );
      }
    } else {
      Object.values(this.authService.formModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  updateConfirmValidator(): void {
    Promise.resolve().then(() =>
      this.authService.formModel.controls[
        'ConfirmPassword'
      ].updateValueAndValidity()
    );
  }

  async onFileSelected(event: any): Promise<void> {
    const file = event.target.files[0];
    if (file) {
      Papa.parse(file, {
        header: true,
        complete: async (results) => {
          console.log(results);
          await this.processCsvData(results.data);
        }, error: () => {
          console.log('all done')
        }
      });
    }
  }

  async processCsvData(data: any[]): Promise<void> {
    for (const user of data) {
      this.authService.formModel.setValue({
        FirstName: user.FirstName,
        LastName: user.LastName,
        CreationDate: user.CreationDate,
        Email: user.Email,
        Password: user.Password,
        ConfirmPassword: user.ConfirmPassword,
        Role: user.Role,
        designationGuid: user.designationGuid,
      });
      await this.handleRegisterModalOk();
    }
    this.authService.formModel.reset();

    if (this.errors.length > 0) {
      this.openErrorModal();
    }
  }

  openErrorModal(): void {
    this.visible = true;
  }

  getRole(role: Roles): string {
    return Roles[role];
  }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
    default: {
      email: AdminValidationConstants.REQUIRED_VALIDATION_MSG,
    },
  };
}
