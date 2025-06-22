/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ExperienceService } from './experience.service';

describe('Service: Experience', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ExperienceService]
    });
  });

  it('should ...', inject([ExperienceService], (service: ExperienceService) => {
    expect(service).toBeTruthy();
  }));
});
