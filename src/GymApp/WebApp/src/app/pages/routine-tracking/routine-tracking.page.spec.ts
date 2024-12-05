import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RoutineTrackingPage } from './routine-tracking.page';

describe('RoutineTrackingPage', () => {
  let component: RoutineTrackingPage;
  let fixture: ComponentFixture<RoutineTrackingPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutineTrackingPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
