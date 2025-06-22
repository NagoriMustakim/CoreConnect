import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { NzModalService, NzModalModule } from 'ng-zorro-antd/modal';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { sharedConstant } from '../../../shared/constants/sharedDocumentConstant';
import { environment } from '../../../../environments/environment';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    NzMessageModule,
    NzFormModule,
    NzInputModule,
    FormsModule,
    NzButtonModule,
    NzIconModule,
    NzCheckboxModule,
    NzModalModule,
    NzFlexModule,
    NzCardModule,
    NzGridModule,
    NzFlexModule
  ],
  animations: [],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', '/src/styles.css'],
})
export class LoginComponent implements OnInit {
  passwordVisible = false;
  isVisible = false;
  loggedIn = false;
  data: object = {};

  constructor(
    public service: AuthService,
    private router: Router,
    private message: NzMessageService
  ) { }

  ngOnInit(): void { }

  autoTips: Record<string, Record<string, string>> = {
    en: {
      required: sharedConstant.REQUIRED_FIELD,
    },
    default: {
      email: sharedConstant.NOT_VALID_EMAIL,
    },
  };

  onSubmit() {
    if (this.service.loginFormModel.valid) {
      this.service.login().subscribe({
        next: (res: any) => {
          console.log(res);
          this.data = res;
          this.service.setToken(res.result);
          const role = this.service.getRole();
          if (role == sharedConstant.ADMIN) {
            this.router.navigateByUrl(environment.routes.dashboard);
          }
          if (role == sharedConstant.HR || role == sharedConstant.RMG || role == sharedConstant.MANAGER) {
            this.router.navigateByUrl(environment.routes.management);
          }
          if (role == sharedConstant.CANDIDATE) {
            this.router.navigateByUrl(environment.routes.candidate);
          }
          this.message.create(sharedConstant.SUCCESS, sharedConstant.LOGIN_SUCCESS);
          this.service.loginFormModel.reset();
        },
        error: (err) => {
          this.message.error(sharedConstant.INVALID_EMAIL_PASSWORD);
          this.service.loginFormModel.reset();
        },
      });
    } else {
      Object.values(this.service.loginFormModel.controls).forEach((control) => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  forgotPasswordSubmit() {
    if (this.service.forgotPaswordFormModel.valid) {
      this.service.forgotPassword().subscribe({
        next: (res) => {
          this.message.success(
            sharedConstant.RESET_PASSWORD_LINK
          );
          this.isVisible = false;
          this.service.forgotPaswordFormModel.reset();
        },
        error: (err) => {
          this.message.error(
            sharedConstant.ERROR_RESET_PASSWORD_LINK
          );
          this.service.forgotPaswordFormModel.reset();
        },
      });
    } else {
      Object.values(this.service.forgotPaswordFormModel.controls).forEach(
        (control) => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        }
      );
    }
  }

  onCancel() {
    this.isVisible = false;
    this.service.forgotPaswordFormModel.reset();
  }

  togglePasswordVisibility(inputElement: HTMLInputElement) {
    if (inputElement.type === sharedConstant.PASSWORD) {
      inputElement.type = sharedConstant.TEXT;
    } else {
      inputElement.type = sharedConstant.PASSWORD;
    }
  }

  openForgotPassword() {
    this.isVisible = true;
  }

  handleOk() {
    this.isVisible = false;
  }
}
