import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanPreviewComponent } from './plan-preview.component';

describe('PlanPreviewComponent', () => {
  let component: PlanPreviewComponent;
  let fixture: ComponentFixture<PlanPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlanPreviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlanPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});