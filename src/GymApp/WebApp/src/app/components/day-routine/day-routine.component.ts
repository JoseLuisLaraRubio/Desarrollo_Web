import { Component, Input, input, OnInit } from "@angular/core";
import { ExerciseSet } from "../../types";
import { NgButtonComponent } from "../ng-button/ng-button.component";
import { Router, RouterModule } from "@angular/router";

@Component({
  selector: "app-day-routine",
  standalone: true,
  imports: [NgButtonComponent, RouterModule],
  templateUrl: "./day-routine.component.html",
  styleUrl: "./day-routine.component.scss",
})
export class DayRoutineComponent{
  @Input() dayNumber!: number;
  @Input() percentage!: number;
  @Input() sets!: ExerciseSet[];
  @Input() action!: any;

  constructor(private router: Router) {}

  startRoutine(){
    this.router.navigate(['/day-routine']);
  }

}
