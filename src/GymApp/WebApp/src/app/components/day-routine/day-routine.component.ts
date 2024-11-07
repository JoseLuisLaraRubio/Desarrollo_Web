import { Component, Input, input, OnInit } from "@angular/core";
import { ExerciseSet } from "../../types";
import { NgButtonComponent } from "../ng-button/ng-button.component";

@Component({
  selector: "app-day-routine",
  standalone: true,
  imports: [NgButtonComponent],
  templateUrl: "./day-routine.component.html",
  styleUrl: "./day-routine.component.scss",
})
export class DayRoutineComponent{
  @Input() dayNumber!: number;
  @Input() percentage!: number;
  @Input() sets!: ExerciseSet[];

}
