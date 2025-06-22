/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MstProjectService } from './mst-project.service';

describe('Service: MstProject', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MstProjectService]
    });
  });

  it('should ...', inject([MstProjectService], (service: MstProjectService) => {
    expect(service).toBeTruthy();
  }));
});
