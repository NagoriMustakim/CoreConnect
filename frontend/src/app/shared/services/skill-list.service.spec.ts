/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SkillListService } from './skill-list.service';

describe('Service: SkillList', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SkillListService]
    });
  });

  it('should ...', inject([SkillListService], (service: SkillListService) => {
    expect(service).toBeTruthy();
  }));
});
