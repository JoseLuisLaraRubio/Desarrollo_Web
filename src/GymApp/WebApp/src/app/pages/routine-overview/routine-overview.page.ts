import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';

interface Routine {
  id: string;
  routines: Workout[];
}

interface Workout {
  id: string;
  blocks: Block[];
}

interface Block {
  id: string;
  exerciseId: string;
  sets: number;
  repetitions: number;
  excerciceName: string;
}
interface Exercise {
  id: string;
  name: string;
  primaryMuscle: string;
  secondaryMuscles: string[];
  equipmentRequirement: string;
  difficultyLevel: number;
  effectivenessLevel: number;
}

@Component({
  selector: 'app-routine-overview',
  standalone: true,
  templateUrl: './routine-overview.page.html',
  styleUrls: ['./routine-overview.page.scss'],
  imports: [UserNavBarComponent],
})
export class RoutineOverviewPage  implements OnInit {

  routine: Routine | undefined;

  constructor(private readonly http: HttpClient) {}

  mockAns = {
    answersIndices: [0, 1]
  };

  ngOnInit() 
  {
    // Send mock answers to initialize the routine
    this.http.post('/api/quiz', this.mockAns);

    this.http.get<Routine[]>('/api/workouts').subscribe(
    {
      next: (response) => {
        this.routine = response[0];

        this.assignExcerciceNames();

        console.log(this.routine)
      },
      error: (err) => {
        console.error('Error fetching workouts:', err);
      }
    });
  }

  assignExcerciceNames()
  {
    var excercises : Exercise[]; 
    this.http.get('api/exercises').subscribe(
      {
        next: (response) => {
          excercises = response as Exercise[];
          console.log(excercises);

          this.routine?.routines.forEach(workout => {
            workout.blocks.forEach(block => {
              console.log(block)
              excercises.forEach(excercice => {
                if(excercice.id === block.exerciseId){
                  block.excerciceName = excercice.name;
                }
              })
            });
          });
        }
      }
    );
  }

}
