import { NzImageModule } from 'ng-zorro-antd/image';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';
import { AuthService } from './../service/auth.service';
import { NzInputModule } from 'ng-zorro-antd/input';
import { CommonModule } from '@angular/common';
import { NzFlexModule } from 'ng-zorro-antd/flex';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidatorFn,
  NonNullableFormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { NzCardModule } from 'ng-zorro-antd/card';
import { sharedConstant } from '../../../shared/constants/sharedDocumentConstant';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [
    NzGridModule,
    NzInputModule,
    NzFormModule,
    NzImageModule,
    NzFlexModule,
    ReactiveFormsModule,
    NzCardModule,
    CommonModule,
    NzButtonModule,
    NzIconModule
  ],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css',
})
export class ResetPasswordComponent implements OnInit {
  token: string = '';
  email: string = '';
  validateForm: FormGroup<{
    password: FormControl<string>;
    checkPassword: FormControl<string>;
  }>;
  passwordVisible = false;
  confirmPasswordVisible = false;
  constructor(
    private service: AuthService,
    private msg: NzMessageService,
    private fb: NonNullableFormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {
    const { required, maxLength, minLength, min, pattern, passwordRegister } =
      CustomValidators;

    this.validateForm = this.fb.group({
      password: ['', [required, minLength(8), passwordRegister()]],
      checkPassword: ['', [required, this.confirmationValidator]],
    });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.token = params['token'];
      this.email = params['email'];
    });
  }
  handleResetPassword() {
    if (this.validateForm.valid) {
      const body = {
        email: this.email,
        token: decodeURIComponent(this.token),
        password: this.validateForm.value.password,
      };
      console.log(body);

      this.service.resetPassword(body).subscribe({
        next: (res) => {
          this.msg.success(sharedConstant.PASSWORD_RESETED);
          this.router.navigate(['/']);
        },
        error: (err) => {
          if (err.error[0].code == sharedConstant.INVALID_TOKEN) {
            this.msg.error(sharedConstant.LINK_EXPIRED);
          } else {
            this.msg.error(err.error[0].description);
          }
          console.log(err.error[0].description);
        },
      });
      console.log(this.validateForm.value, this.token, this.email);
    }
  }
  confirmationValidator: ValidatorFn = (
    control: AbstractControl
  ): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  };
  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() =>
      this.validateForm.controls.checkPassword.updateValueAndValidity()
    );
  }
  onSubmit() {
    if (this.validateForm.valid) {
      // Implement your logic to send the new password and token to your backend service
      console.log(this.validateForm.value);
    }
  }
}
