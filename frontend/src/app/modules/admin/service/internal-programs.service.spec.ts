import { TestBed } from '@angular/core/testing';

import { InternalProgramsService } from './internal-programs.service';

describe('InternalProgramsService', () => {
  let service: InternalProgramsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternalProgramsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
