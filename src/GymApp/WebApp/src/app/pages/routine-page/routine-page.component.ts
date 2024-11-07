import { Component, OnInit } from "@angular/core";
import { NgButtonComponent } from "../../components/ng-button/ng-button.component";
import { DayRoutine } from "../../types/day-routine";
import { weeklyRoutine } from "../../data/day-routine-data";
import { DayRoutineComponent } from "../../components/day-routine/day-routine.component";
import { HomeNavBarComponent } from "../../components/home-nav-bar/home-nav-bar.component";

@Component({
  selector: "app-routine-page",
  standalone: true,
  imports: [
    NgButtonComponent,
    DayRoutineComponent,
    DayRoutineComponent,
    HomeNavBarComponent,
  ],
  templateUrl: "./routine-page.component.html",
  styleUrl: "./routine-page.component.scss",
})
export class RoutinePageComponent implements OnInit {
  private readonly _routines: DayRoutine[];
  itemsWidth!: string;
  editting = false;

  constructor() {
    this._routines = weeklyRoutine;
  }

  ngOnInit(): void {
    this.itemsWidth =
      (window.innerWidth / this._routines.length).toString() + "px";
  }

  editRoutine() {
    this.editting = true;
  }

  saveChanges() {
    this.editting = false;
  }

  cancel() {
    this.editting = false;
  }

  public get routines() {
    return this._routines;
  }
}
