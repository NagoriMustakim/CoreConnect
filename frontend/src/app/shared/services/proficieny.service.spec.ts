import { TestBed } from '@angular/core/testing';

import { ProficienyService } from './proficieny.service';

describe('ProficienyService', () => {
  let service: ProficienyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProficienyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
