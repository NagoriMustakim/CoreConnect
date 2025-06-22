import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternalProgramsComponent } from './internal-programs.component';

describe('InternalProgramsComponent', () => {
  let component: InternalProgramsComponent;
  let fixture: ComponentFixture<InternalProgramsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InternalProgramsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InternalProgramsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
