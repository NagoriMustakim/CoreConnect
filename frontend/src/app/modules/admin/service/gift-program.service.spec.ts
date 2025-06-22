/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GiftProgramService } from './gift-program.service';

describe('Service: GiftProgram', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GiftProgramService]
    });
  });

  it('should ...', inject([GiftProgramService], (service: GiftProgramService) => {
    expect(service).toBeTruthy();
  }));
});
