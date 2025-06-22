import { TestBed } from '@angular/core/testing';

import { TrainingNameService } from './training-name.service';

describe('TrainingNameService', () => {
  let service: TrainingNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
