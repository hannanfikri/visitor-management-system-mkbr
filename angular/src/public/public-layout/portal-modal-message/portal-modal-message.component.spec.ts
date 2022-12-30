import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortalModalMessageComponent } from './portal-modal-message.component';

describe('PortalModalMessageComponent', () => {
  let component: PortalModalMessageComponent;
  let fixture: ComponentFixture<PortalModalMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PortalModalMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PortalModalMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
