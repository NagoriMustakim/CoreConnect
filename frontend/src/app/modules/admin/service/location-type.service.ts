import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  Form,
  FormGroup,
  NonNullableFormBuilder,
  Validators,
} from '@angular/forms';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'any',
})
export class LocationTypeService {
  readonly baseURI = environment.apiUrl+environment.routes.workModels;
  createLocationTypeModel!: FormGroup;
  editLocationTypeModel!: FormGroup;
  isEditMode = false;

  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;

    this.createLocationTypeModel = this.fb.group({
      locationType: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      locationTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
    this.editLocationTypeModel = this.fb.group({
      locationTypeGuid: ['', required],
      locationType: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      locationTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  getLocaitonTypes() {
    return this.http.get(this.baseURI);
  }

  createLocationType() {
    let body = {
      locationType: this.createLocationTypeModel.value.locationType,
      locationTypeDescription:
        this.createLocationTypeModel.value.locationTypeDescription,
    };
    console.log(body);

    return this.http.post(this.baseURI, body);
  }

  populateEditForm(locationType: any): void {
    this.editLocationTypeModel.patchValue({
      locationTypeGuid: locationType.locationTypeGuid,
      locationType: locationType.locationType,
      locationTypeDescription: locationType.locationTypeDescription,
    });
    this.isEditMode = true;
  }

  updateLocationType(): any {
    return this.http.put(this.baseURI, {
      locationTypeGuid: this.editLocationTypeModel.value.locationTypeGuid,
      locationType: this.editLocationTypeModel.value.locationType,
      locationTypeDescription:
        this.editLocationTypeModel.value.locationTypeDescription,
    });
  }

  deleteLocationType(id: string) {
    return this.http.delete(this.baseURI + '/' + id);
  }
}
