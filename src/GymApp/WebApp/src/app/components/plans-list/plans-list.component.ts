import { Component, Input, OnInit, ViewChild, ElementRef } from '@angular/core';
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
  itemWidth: number = 200;
  offset: number = -20;  

  @ViewChild('plansContainer') plansList!: ElementRef;

  constructor(){}
  
  ngOnInit(): void {
    this.listName = this.listName.toUpperCase();
    this.plans = ['Plan 1', 'Plan 2', 'Plan 3', 'Plan 4', 'Plan 5'];
  }

  scrollLeft(){
    if(this.plansList != null){
      this.offset -= this.itemWidth;
      this.plansList.nativeElement.style.marginLeft = this.offset.toString() + "px";
    }
  }
  
  scrollRight(){
    if(this.plansList != null){
      this.offset += this.itemWidth;
      this.plansList.nativeElement.style.marginLeft = this.offset.toString() + "px";
    }
  }
}
