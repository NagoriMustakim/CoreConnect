import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup, NonNullableFormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'root',
})
export class TrainingTypeService {
  createTrainingTypeModel!: FormGroup;
  editTrainingTypeModel!: FormGroup;
  isEditMode = false;
  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;
    this.createTrainingTypeModel = this.fb.group({
      trainingType: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      trainingTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
    this.editTrainingTypeModel = this.fb.group({
      trainingTypeGuid: ['', required],
      trainingType: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      trainingTypeDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

  readonly baseURI = environment.apiUrl+environment.routes.trainings;

  getTrainingType() {
    return this.http.get(this.baseURI);
  }

  createTrainingType() {
    let body = {
      trainingType: this.createTrainingTypeModel.value.trainingType,
      trainingTypeDescription: this.createTrainingTypeModel.value.trainingTypeDescription,
    };
    return this.http.post(this.baseURI, body);
  }
  populateEditForm(locationType: any): void {
    this.editTrainingTypeModel.patchValue({
      trainingTypeGuid: locationType.trainingTypeGuid,
      trainingType: locationType.trainingType,
      trainingTypeDescription: locationType.trainingTypeDescription,
    });
    this.isEditMode = true;
  }
  updateTrainingType(): any {
    return this.http.put(this.baseURI, {
      trainingTypeGuid: this.editTrainingTypeModel.value.trainingTypeGuid,
      trainingType: this.editTrainingTypeModel.value.trainingType,
      trainingTypeDescription: this.editTrainingTypeModel.value.trainingTypeDescription,
    });
  }
  editTrainingType(id: string, body: any) {
    return this.http.put(this.baseURI + '/' + id, body);
  }

  deleteTrainingType(id: string) {

    return this.http.delete(this.baseURI + '/' + id);
  }
}
