import { TestBed } from '@angular/core/testing';

import { NominationServiceService } from './nomination-service.service';

describe('NominationServiceService', () => {
  let service: NominationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NominationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
