import { Component } from '@angular/core';
import { DayRoutineComponent } from '../../components/day-routine/day-routine.component';
import { DayRoutine } from '../../types';
import { weeklyRoutine } from '../../data/day-routine-data';
import { NavBarComponent } from '../../components/nav-bar/nav-bar.component';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [DayRoutineComponent, NavBarComponent],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent {
  private readonly _routines : DayRoutine[];

  constructor(){
    this._routines = weeklyRoutine;
  }

  public get routines() {
    return this._routines;
  }
}
