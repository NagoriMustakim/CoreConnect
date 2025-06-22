/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ComponyService } from './compony.service';

describe('Service: Compony', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComponyService]
    });
  });

  it('should ...', inject([ComponyService], (service: ComponyService) => {
    expect(service).toBeTruthy();
  }));
});
