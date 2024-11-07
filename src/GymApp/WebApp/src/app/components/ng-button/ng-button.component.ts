import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-ng-button',
  standalone: true,
  imports: [CommonModule],
  template: `
    <input type="button" 
    [value]="value" [ngStyle]="{'border-color': color}">
  `,
  styleUrl: './ng-button.component.scss'
})
export class NgButtonComponent implements OnInit{
  @Input() value!: string;
  @Input() color: string | undefined;

  ngOnInit(): void {
    if (!this.color) {
      this.color = "#CA2529";
    }
  }

}
