import { TestBed } from '@angular/core/testing';

import { NoiminationService } from './noimination.service';

describe('NoiminationService', () => {
  let service: NoiminationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NoiminationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
