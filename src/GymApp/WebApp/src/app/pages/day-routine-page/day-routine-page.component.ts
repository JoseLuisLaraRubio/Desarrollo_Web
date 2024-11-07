import { Component } from '@angular/core';
import { ExcerciseCardComponent } from '../../components/excercise-card/excercise-card.component';
import { NgButtonComponent } from '../../components/ng-button/ng-button.component';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-day-routine-page',
  standalone: true,
  imports: [ExcerciseCardComponent, NgButtonComponent, RouterModule],
  templateUrl: './day-routine-page.component.html',
  styleUrl: './day-routine-page.component.scss'
})
export class DayRoutinePageComponent {
  constructor(private router: Router) {}

  cancelRoutine(){
    this.router.navigate(['/routine']);
  }
  finishRoutine(){
    this.router.navigate(['/routine']);
  }
}
