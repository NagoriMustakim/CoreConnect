/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MstCertificationService } from './mst-certification.service';

describe('Service: Certification', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MstCertificationService]
    });
  });

  it('should ...', inject([MstCertificationService], (service: MstCertificationService) => {
    expect(service).toBeTruthy();
  }));
});
