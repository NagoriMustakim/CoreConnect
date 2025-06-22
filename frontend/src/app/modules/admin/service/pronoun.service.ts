import { environment } from './../../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  FormGroup,
  NonNullableFormBuilder,
} from '@angular/forms';
import { CustomValidators } from '../../../shared/helpers/CustomValidators';
import { AdminValidationConstants } from '../constants/AdminValidationConstants';

@Injectable({
  providedIn: 'root',
})
export class PronounService {
  createPronounModel!: FormGroup;
  editPronounModel!: FormGroup;
  isEditMode = false;
  constructor(private fb: NonNullableFormBuilder, private http: HttpClient) {
    const { required, maxLength, pattern } = CustomValidators;

    this.createPronounModel = this.fb.group({
      pronoun: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS_BACKSLASH_SPACE)]],
      pronounDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
    this.editPronounModel = this.fb.group({
      pronounGuid: ['', required],
      pronoun: ['', [required, maxLength(50), pattern(AdminValidationConstants.REGEX_PATTERN_ALPHABETS_BACKSLASH_SPACE)]],
      pronounDescription: ['', [maxLength(500), pattern(AdminValidationConstants.REGEX_PATTERN_NO_ANGELBRACKETS)]],
    });
  }
  readonly baseURI = environment.apiUrl+environment.routes.pronouns;

  getPronouns() {
    return this.http.get(this.baseURI);
  }

  createPronoun() {
    let body = {
      pronoun: this.createPronounModel.value.pronoun,
      pronounDescription: this.createPronounModel.value.pronounDescription,
    };
    return this.http.post(this.baseURI, body);
  }

  populateEditForm(pronoun: any): void {
    this.editPronounModel.patchValue({
      pronounGuid: pronoun.pronounGuid,
      pronoun: pronoun.pronoun,
      pronounDescription: pronoun.pronounDescription,
    });
    this.isEditMode = true;
  }

  updatePronoun(): any {
    if (this.editPronounModel.valid) {
      return this.http.put(
        this.baseURI + '/' + this.editPronounModel.value.pronounGuid,
        {
          pronoun: this.editPronounModel.value.pronoun,
          pronounDescription: this.editPronounModel.value.pronounDescription,
        }
      );
    }
  }

  deletePronoun(id: string) {
    return this.http.delete(this.baseURI + '/' + id);
  }
}
