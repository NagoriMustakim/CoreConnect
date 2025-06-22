/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LanguageListService } from './language-list.service';

describe('Service: LanguageList', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LanguageListService]
    });
  });

  it('should ...', inject([LanguageListService], (service: LanguageListService) => {
    expect(service).toBeTruthy();
  }));
});
