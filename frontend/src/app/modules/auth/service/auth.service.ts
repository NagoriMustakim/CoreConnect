import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { jwtDecode } from 'jwt-decode';
import { runInThisContext } from 'vm';
import { ReturnStatement } from '@angular/compiler';
import { userInfo } from 'os';
import { environment } from '../../../../environments/environment';
import { sharedConstant } from '../../../shared/constants/sharedDocumentConstant';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  formModel!: FormGroup;
  loginFormModel!: FormGroup;
  forgotPaswordFormModel: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient) {
    const { minLength, maxLength, required, passwordRegister, email, pattern } = CustomValidators;
    this.formModel = this.fb.group(
      {
        FirstName: ['', [required, maxLength(50), pattern(sharedConstant.NAME_REGEX)]],
        LastName: ['', [required, maxLength(50), pattern(sharedConstant.NAME_REGEX)]],
        Role: ['', required],
        Email: ['', email()],
        Password: ['', [required, minLength(8), passwordRegister()]],
        ConfirmPassword: ['', [required, this.confirmationValidator]],
        CreationDate: ['', [required]],
        designationGuid: ['', [required]]
      }

    );
    this.loginFormModel = this.fb.group({
      Email: ['', [required, email()]],
      Password: ['', [required]],
    });
    this.forgotPaswordFormModel = this.fb.group({
      email: ['', email()]
    })
  }

  readonly baseURI = environment.apiUrl;

  confirmationValidator: ValidatorFn = (
    control: AbstractControl
  ): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.formModel.controls['Password'].value) {
      return { confirm: true, error: true };
    }
    return {};
  };



  login() {
    let body = {
      Email: this.loginFormModel.value.Email,
      Password: this.loginFormModel.value.Password,
    };
    return this.http.post(this.baseURI + environment.routes.accounts + environment.routes.login, body);
  }

   register() {
    let body = {
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      Email: this.formModel.value.Email,
      Role: this.formModel.value.Role,
      Password: this.formModel.value.Password,
      ConfirmPassword: this.formModel.value.ConfirmPassword,
      CreationDate: new Date(this.formModel.value.CreationDate).toISOString(),
      designationGuid: this.formModel.value.designationGuid
    };
    console.log(body);

    return this.http.post(this.baseURI + environment.routes.accounts + environment.routes.signUp, body);
  }

  forgotPassword() {
    const body = { email: this.forgotPaswordFormModel.value.email }
    return this.http.post(this.baseURI + environment.routes.accounts + environment.routes.forgotPassword, body)
  }

  resetPassword(data: any) {
    return this.http.post(this.baseURI + environment.routes.accounts + environment.routes.resetPasword, data)
  }

  changePassword(userId: string, newPassword: string) {
    return this.http.post(this.baseURI + environment.routes.accounts + environment.routes.changePassword + '/' + userId, { newPassword });
  }
  getToken() {
    if (typeof window !== undefined) {
      const token = window.localStorage.getItem(sharedConstant.TOKEN);
      return token;
    }

    return null;
  }
  setToken(token: string) {
    try {
      localStorage.setItem(sharedConstant.TOKEN, token);
      return true;
    } catch {
      return false;
    }
  }
  deleteToken() {
    try {
      localStorage.removeItem(sharedConstant.TOKEN);
      return true;
    } catch {
      return false;
    }
  }
  decodeToken() {
    const token = this.getToken();
    let decodedToken;
    if (token) {
      decodedToken = jwtDecode(token);
    }
    return decodedToken;
  }
  getRole() {
    const decodedToken: any = this.decodeToken();
    let role;
    if (decodedToken) {
      role =
        decodedToken[
        sharedConstant.DECODE_TOKEN_ROLE
        ];
    }
    return role;
  }
  getUserId(): string {
    const decodedToken: any = this.decodeToken();
    let userId: string;

    userId =
      decodedToken[
      sharedConstant.DECODE_TOKEN_NAME_IDENTIFIER
      ];
    return userId;
  }
  getExpiry() {
    const decodedToken = this.decodeToken();
    let expiry;
    if (decodedToken) {
      expiry = decodedToken.exp;
    }
    if (expiry) return expiry;
    return 0;
  }
}
