/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BasicDetailService } from './basic-detail.service';

describe('Service: BasicDetail', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BasicDetailService]
    });
  });

  it('should ...', inject([BasicDetailService], (service: BasicDetailService) => {
    expect(service).toBeTruthy();
  }));
});
