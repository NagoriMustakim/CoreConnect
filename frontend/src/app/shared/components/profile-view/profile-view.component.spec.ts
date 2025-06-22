import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PROFILEVIEWComponent } from './profile-view.component';

describe('PROFILEVIEWComponent', () => {
  let component: PROFILEVIEWComponent;
  let fixture: ComponentFixture<PROFILEVIEWComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PROFILEVIEWComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PROFILEVIEWComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
