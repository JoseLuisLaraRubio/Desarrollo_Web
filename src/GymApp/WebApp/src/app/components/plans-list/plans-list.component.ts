import { Component, Input, OnInit } from '@angular/core';
import { PlanPreviewComponent } from "../plan-preview/plan-preview.component";

@Component({
  selector: 'app-plans-list',
  standalone: true,
  imports: [PlanPreviewComponent],
  templateUrl: './plans-list.component.html',
  styleUrl: './plans-list.component.scss'
})
export class PlansListComponent implements OnInit {
  @Input({required:true}) listName!: string;
  plans!: string[];

  constructor(){}

  ngOnInit(): void {
      this.plans = ['Plan 1', 'Plan 2', 'Plan 3', 'Plan 4', 'Plan 5']
  }
}
