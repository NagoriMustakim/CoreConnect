/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { InternalProgramCategoriesService } from './internal-program-categories.service';

describe('Service: InternalProgramCategories', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InternalProgramCategoriesService]
    });
  });

  it('should ...', inject([InternalProgramCategoriesService], (service: InternalProgramCategoriesService) => {
    expect(service).toBeTruthy();
  }));
});
