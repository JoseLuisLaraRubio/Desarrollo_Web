import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutinePageComponent } from './routine-page.component';
import { NgButtonComponent } from '../../components/ng-button/ng-button.component';

describe('RoutinePageComponent', () => {
  let component: RoutinePageComponent;
  let fixture: ComponentFixture<RoutinePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoutinePageComponent, NgButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoutinePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
