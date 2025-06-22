/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { GiftProgramComponent } from './gift-program.component';

describe('GiftProgramComponent', () => {
  let component: GiftProgramComponent;
  let fixture: ComponentFixture<GiftProgramComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GiftProgramComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GiftProgramComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
