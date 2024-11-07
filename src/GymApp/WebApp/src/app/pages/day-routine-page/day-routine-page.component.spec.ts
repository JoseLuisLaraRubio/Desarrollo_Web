import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DayRoutinePageComponent } from './day-routine-page.component';

describe('DayRoutinePageComponent', () => {
  let component: DayRoutinePageComponent;
  let fixture: ComponentFixture<DayRoutinePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DayRoutinePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DayRoutinePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
