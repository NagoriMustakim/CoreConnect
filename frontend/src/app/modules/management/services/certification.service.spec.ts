/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CertificationService } from './certification.service';

describe('Service: Certification', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CertificationService]
    });
  });

  it('should ...', inject([CertificationService], (service: CertificationService) => {
    expect(service).toBeTruthy();
  }));
});
