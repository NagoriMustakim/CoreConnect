import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  FormGroup,
  NonNullableFormBuilder,
} from '@angular/forms';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';
import { AdminFormdataConstants } from '../constants/AdminFormdataConstants';

@Injectable({
  providedIn: 'root',
})
export class BusinessUnitService {
  isEditMode = false;
  private url = environment.apiUrl + environment.routes.businessUnits;

  createBusinessUnitForm!: FormGroup;
  editBusinessUnitForm!: FormGroup;

  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { pattern, required, maxLength } = CustomValidators
    this.createBusinessUnitForm = this.fb.group({
      businessUnitGuid: [''],
      businessUnitName: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN)]],
      businessUnitLogoName: ['', [required]],
      businessUnitDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });

    this.editBusinessUnitForm = this.fb.group({
      businessUnitGuid: [''],
      businessUnitName: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_BRACKETS_HYPHEN)]],
      businessUnitLogoName: [''],
      businessUnitDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  getBusinessUnits() {
    return this.http.get(this.url);
  }

  createBusinessUnit(businessUnit: any, fileList: NzUploadFile[]) {
    const formData = new FormData();
    formData.append(AdminFormdataConstants.BUSINESSUNIT_NAME, businessUnit.businessUnitName);
    formData.append(AdminFormdataConstants.BUSINESSUNIT_DESCRIPTION, businessUnit.businessUnitDescription);

    fileList.forEach((file: any) => {
      formData.append(AdminFormdataConstants.FILE, file);
    });

    return this.http.post(this.url, formData);
  }

  populateEditForm(businessUnit: any): void {
    this.editBusinessUnitForm.patchValue({
      businessUnitGuid: businessUnit.businessUnitGuid,
      businessUnitLogoName: businessUnit.businessUnitLogoName,
      businessUnitName: businessUnit.businessUnitName,
      businessUnitDescription: businessUnit.businessUnitDescription,
    });

    this.isEditMode = true;
  }

  updateBusinessUnit(businessUnit: any, fileList: NzUploadFile[]) {
    const formData = new FormData();
    formData.append(AdminFormdataConstants.BUSINESSUNIT_GUID, businessUnit.businessUnitGuid);
    formData.append(AdminFormdataConstants.BUSINESSUNIT_LOGO_NAME, businessUnit.businessUnitLogoName);
    formData.append(AdminFormdataConstants.BUSINESSUNIT_NAME, businessUnit.businessUnitName);
    formData.append(AdminFormdataConstants.BUSINESSUNIT_DESCRIPTION, businessUnit.businessUnitDescription);

    fileList.forEach((file: any) => {
      formData.append(AdminFormdataConstants.FILE, file);
    });

    return this.http.put(
      this.url + '/' + this.editBusinessUnitForm.value.businessUnitGuid, formData);
  }

  deleteBusinessUnit(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
