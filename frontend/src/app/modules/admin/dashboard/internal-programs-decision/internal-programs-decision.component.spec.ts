import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternalProgramsDecisionComponent } from './internal-programs-decision.component';

describe('InternalProgramsDecisionComponent', () => {
  let component: InternalProgramsDecisionComponent;
  let fixture: ComponentFixture<InternalProgramsDecisionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternalProgramsDecisionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InternalProgramsDecisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
