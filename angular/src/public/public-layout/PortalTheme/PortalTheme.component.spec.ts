/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PortalThemeComponent } from './PortalTheme.component';

describe('PortalThemeComponent', () => {
  let component: PortalThemeComponent;
  let fixture: ComponentFixture<PortalThemeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortalThemeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortalThemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
