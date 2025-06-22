/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ComponyComponent } from './compony.component';

describe('ComponyComponent', () => {
  let component: ComponyComponent;
  let fixture: ComponentFixture<ComponyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComponyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComponyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
