import { TestBed } from '@angular/core/testing';

import { GiftProgramService } from './gift-program.service';

describe('GiftProgramService', () => {
  let service: GiftProgramService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GiftProgramService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
