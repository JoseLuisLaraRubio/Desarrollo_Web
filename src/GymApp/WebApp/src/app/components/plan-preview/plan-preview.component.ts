import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-plan-preview',
  standalone: true,
  imports: [],
  templateUrl: './plan-preview.component.html',
  styleUrl: './plan-preview.component.scss'
})
export class PlanPreviewComponent implements OnInit{
  @Input() imgSource!: string;
  @Input() planName!: string;

  ngOnInit(): void {
      this.planName = this.planName.toUpperCase();
  }
}
