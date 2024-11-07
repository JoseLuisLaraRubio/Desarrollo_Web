import { Component, OnInit } from '@angular/core';
import { NgButtonComponent } from "../../components/ng-button/ng-button.component";
import { NavBarComponent } from "../../components/nav-bar/nav-bar.component";
import { DayRoutine } from '../../types/day-routine';
import { weeklyRoutine } from '../../data/day-routine-data';
import { DayRoutineComponent } from "../../components/day-routine/day-routine.component";

@Component({
  selector: 'app-routine-page',
  standalone: true,
  imports: [NgButtonComponent, NavBarComponent, DayRoutineComponent, DayRoutineComponent],
  templateUrl: './routine-page.component.html',
  styleUrl: './routine-page.component.scss'
})
export class RoutinePageComponent implements OnInit{
  private readonly _routines : DayRoutine[];
  itemsWidth!: string;

  constructor(){
    this._routines = weeklyRoutine;
  }

  ngOnInit(): void {
    this.itemsWidth = (window.innerWidth / this._routines.length).toString() + 'px';
  }

  public get routines() {
    return this._routines;
  }
}
