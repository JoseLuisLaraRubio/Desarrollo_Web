import { Component, input } from "@angular/core";
import { ExerciseSet } from "../../types";

@Component({
  selector: "app-day-routine",
  standalone: true,
  imports: [],
  templateUrl: "./day-routine.component.html",
  styleUrl: "./day-routine.component.scss",
})
export class DayRoutineComponent {
  dayNumber = input<number>();
  sets = input<ExerciseSet[]>();
}
