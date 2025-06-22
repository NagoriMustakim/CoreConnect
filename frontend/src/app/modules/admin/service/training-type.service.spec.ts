/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TrainingTypeService } from './training-type.service';

describe('Service: TrainingType', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TrainingTypeService]
    });
  });

  it('should ...', inject([TrainingTypeService], (service: TrainingTypeService) => {
    expect(service).toBeTruthy();
  }));
});
