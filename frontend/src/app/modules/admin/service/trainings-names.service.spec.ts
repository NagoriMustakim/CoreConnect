import { TestBed } from '@angular/core/testing';

import { TrainingsNamesService } from './trainings-names.service';

describe('TrainingsNamesService', () => {
  let service: TrainingsNamesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingsNamesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
