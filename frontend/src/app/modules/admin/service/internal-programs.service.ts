import { HttpClient } from '@angular/common/http';
import { FormGroup, NonNullableFormBuilder } from '@angular/forms';
import { Injectable } from '@angular/core';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { environment } from '../../../../environments/environment';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'root',
})
export class InternalProgramsService {
  readonly baseURI = environment.apiUrl + environment.routes.internalprogram;
  createInternalProgramsModel!: FormGroup;
  editInternalProgramsModel!: FormGroup;
  isEditMode = false;

  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;

    this.createInternalProgramsModel = this.fb.group({
      internalProgramName: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      internalProgramReviewCycle: ['', [required, maxLength(2)]],
      internalProgramActiveDays: ['', [required, maxLength(2)]],
      isInternalProgramCategoryExists: [false, [required]],
      internalProgramDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });

    this.editInternalProgramsModel = this.fb.group({
      internalProgramGuid: ['', required],
      internalProgramName: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      internalProgramReviewCycle: ['', [required, maxLength(2)]],
      internalProgramActiveDays: ['', [required, maxLength(2)]],
      isInternalProgramCategoryExists: [false, [required]],
      internalProgramDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }

    getInternalPrograms() {
    return this.http.get(this.baseURI);
    }

  getInternalProgramById(internalProgramId: string) {
    return this.http.get(`${this.baseURI}/${internalProgramId}`)
  }

  createInternalProgram() {
    return this.http.post(this.baseURI, this.createInternalProgramsModel.value);
  }

  populateEditForm(internalProgram: any): void {
    this.editInternalProgramsModel.patchValue(internalProgram);
    this.isEditMode = true;
  }

  updateInternalProgram(): any {
    return this.http.put(this.baseURI + '/' + this.editInternalProgramsModel.value.internalProgramGuid, this.editInternalProgramsModel.value);
  }

  deleteInternalPrograms(id: string) {
    return this.http.delete(this.baseURI + '/' + id);
  }
}
