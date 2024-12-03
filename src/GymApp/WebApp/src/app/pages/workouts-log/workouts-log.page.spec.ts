import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WorkoutsLogPage } from './workouts-log.page';

describe('WorkoutsLogPage', () => {
  let component: WorkoutsLogPage;
  let fixture: ComponentFixture<WorkoutsLogPage>;

  beforeEach(() => {
    fixture = TestBed.createComponent(WorkoutsLogPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
