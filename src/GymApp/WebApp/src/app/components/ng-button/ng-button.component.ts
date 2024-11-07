import { Component, Input } from '@angular/core';


@Component({
  selector: 'app-ng-button',
  standalone: true,
  imports: [],
  template: `
    <input type="button" 
    [value]="value"
    [style.border-color]="color"
    [style.background-color]="isHovered ? color : 'transparent'"
    (mouseenter)="onHover(true)"
    (mouseleave)="onHover(false)">
  `,
  styleUrl: './ng-button.component.scss'
})
export class NgButtonComponent{
  @Input() value!: string;
  @Input() color!: string;

  isHovered = false;

  onHover(hovered: boolean) {
    this.isHovered = hovered;
  }
}
