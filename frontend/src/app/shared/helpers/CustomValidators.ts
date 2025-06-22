import { AbstractControl, ValidatorFn, Validators } from '@angular/forms';
import { NzSafeAny } from 'ng-zorro-antd/core/types';

export type MyErrorsOptions = { en: string } & Record<string, NzSafeAny>;
export type MyValidationErrors = Record<string, MyErrorsOptions>;

export class CustomValidators extends Validators {
  static override minLength(minLength: number): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.minLength(minLength)(control) === null) {
        return null;
      }
      return {
        minlength: { en: `Minimun ${minLength} charecters are required.` },
      };
    };
  }
  static override maxLength(maxLength: number): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.maxLength(maxLength)(control) === null) {
        return null;
      }
      return {
        maxlength: { en: `Maximum ${maxLength} characters are allowed.` },
      };
    };
  }
  static override min(min: number): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.min(min)(control) === null) {
        return null;
      }
      return { min: { en: `Minimum ${min} value is required.` } };
    };
  }

  static override pattern(pattern: string): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      if (Validators.pattern(pattern)(control) === null) {
        return null;
      }
      return { pattern: { en: `Invalid input` } };
    };
  }

  static override email(): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      if (emailRegex.test(control.value)) {
        return null;
      }
      return { emailId: { en: `Please enter valid email` } };
    };
  }

  static passwordRegister(): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      const passwordRegex =
        '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$';
      if (Validators.pattern(passwordRegex)(control) === null) {
        return null;
      }
      return {
        password: {
          en: 'Invalid password format. The password must contain minimum a uppercase, a lowercase, a speacial charecter and a number and leangth of 8 charecters.',
        },
      };
    };
  }

  static alphabates(): ValidatorFn {
    return (control: AbstractControl): MyValidationErrors | null => {
      const alphaPattern = /^[A-Za-z\/\.,'"]+$/;
      if (alphaPattern.test(control.value)) {
        return null;
      }
      return {
        alphabates: {
          en: 'Please enter valid charecters',
        },
      };
    };
  }
}
