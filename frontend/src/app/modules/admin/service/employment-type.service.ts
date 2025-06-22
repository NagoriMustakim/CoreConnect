import { EmploymentType } from './../interfaces/employmentType';
import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormGroup, NonNullableFormBuilder } from '@angular/forms';
import { Injectable } from '@angular/core';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'root',
})
export class EmploymentTypeService {
  readonly baseURI = environment.apiUrl+environment.routes.employmentTypes;
  createEmploymentTypeModel!: FormGroup;
  editEmploymentTypeModel!: FormGroup;
  isEditMode = false;

  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;
    this.createEmploymentTypeModel = this.fb.group({
      employmentTypeTitle: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      employmentTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
    this.editEmploymentTypeModel = this.fb.group({
      employmentTypeGuid: ['', required],
      employmentTypeTitle: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      employmentTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }
  getEmploymentTypes() {
    return this.http.get<EmploymentType[]>(this.baseURI);
  }
  createEmploymentType() {
    let body = {
      employmentTypeTitle:
        this.createEmploymentTypeModel.value.employmentTypeTitle,
      employmentTypeDescription:
        this.createEmploymentTypeModel.value.employmentTypeDescription,
    };
    return this.http.post(this.baseURI, body);
  }
  populateEditForm(employmentType: any): void {
    this.editEmploymentTypeModel.patchValue({
      employmentTypeGuid: employmentType.employmentTypeGuid,
      employmentTypeTitle: employmentType.employmentTypeTitle,
      employmentTypeDescription: employmentType.employmentTypeDescription,
    });
    this.isEditMode = true;
    console.log(this.editEmploymentTypeModel.value);
  }

  updateEmploymentType(): any {
    return this.http.put(this.baseURI, {
      employmentTypeGuid: this.editEmploymentTypeModel.value.employmentTypeGuid,
      employmentTypeTitle:
        this.editEmploymentTypeModel.value.employmentTypeTitle,
      employmentTypeDescription:
        this.editEmploymentTypeModel.value.employmentTypeDescription,
    });
  }
  deleteEmploymentType(id: string) {
    return this.http.delete(this.baseURI + '/' + id);
  }
}
