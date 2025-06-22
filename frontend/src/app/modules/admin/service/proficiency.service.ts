import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup, NonNullableFormBuilder } from '@angular/forms';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'any',
})
export class ProficiencyService {
  readonly baseURI = environment.apiUrl+environment.routes.proficiency;
  createProficiencyModel!: FormGroup;
  editProficiencyModel!: FormGroup;
  isEditMode = false;

  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;

    this.createProficiencyModel = this.fb.group({
      proficiencyTitle: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      proficiencyDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
    this.editProficiencyModel = this.fb.group({
      proficiencyGuid: ['', required],
      proficiencyTitle: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHANUMERIC_UNDERSCORE_DASH_DOT)]],
      proficiencyDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }
  getProficiencyType() {
    return this.http.get(this.baseURI);
  }
  createProficiencyType() {
    let body = {
      proficiencyTitle: this.createProficiencyModel.value.proficiencyTitle,
      proficiencyDescription:
        this.createProficiencyModel.value.proficiencyDescription,
    };
    console.log(body);

    return this.http.post(this.baseURI, body);
  }

  populateEditForm(proficiency: any): void {
    this.editProficiencyModel.patchValue({
      proficiencyGuid: proficiency.proficiencyGuid,
      proficiencyTitle: proficiency.proficiencyTitle,
      proficiencyDescription: proficiency.proficiencyDescription,
    });
    this.isEditMode = true;
  }
  updateProficiencyType(): any {
    return this.http.put(this.baseURI, {
      proficiencyGuid: this.editProficiencyModel.value.proficiencyGuid,
      proficiencyTitle: this.editProficiencyModel.value.proficiencyTitle,
      proficiencyDescription:
        this.editProficiencyModel.value.proficiencyDescription,
    });
  }

  deleteProficiencyType(id: string) {
    return this.http.delete(this.baseURI + '/' + id);
  }
}
