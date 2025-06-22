import { TestBed } from '@angular/core/testing';

import { InternalprogramService } from './internalprogram.service';

describe('InternalprogramService', () => {
  let service: InternalprogramService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternalprogramService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
