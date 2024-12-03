import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';
import { switchMap } from 'rxjs';
import { FormsModule } from '@angular/forms';

interface Routine {
  id: string;
  workouts: Workout[];
}

interface Workout {
  id: string;
  blocks: Block[];
}

interface Block {
  id: string;
  exercise: Exercise;
  sets: number;
  repetitions: number;
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
  imports: [UserNavBarComponent, FormsModule],
})
export class RoutineOverviewPage  implements OnInit {

  searchInput: string = "";
  searching: boolean = false;

  routine: Routine | undefined;
  selectedWorkout: Workout | undefined;

  exercises: Exercise[] = [];
  matchingExercises: Exercise[] = [];

  constructor(private readonly http: HttpClient) {}

  mockAns = {
    answersIndices: [0, 1]
  };

  ngOnInit()
  {
    // Send mock answers and ensure the GET request only proceeds after POST completes
    this.http.post('/api/quiz', this.mockAns).pipe(
      switchMap(() => this.http.get<Routine>('/api/routines/current'))
    ).subscribe(
      (response) => {
        this.routine = response;
      }
    );

    this.http.get<Exercise[]>('/api/exercises').subscribe(
      (response) => {
        this.exercises = response;
      }
    );
  }

  openEditForm(workout: Workout)
  {
    this.selectedWorkout = workout;
  }
  
  closeModal(){
    this.selectedWorkout = undefined;
  }

  findExcercices(){
    if(this.exercises.length == 0 || this.searchInput == ""){
      this.searching = false;
      return;
    }

    this.searching = true;

    this.matchingExercises = [];
    this.exercises.forEach(exercise => {
      if(exercise.name.toLowerCase().includes(this.searchInput.toLowerCase())){
        this.matchingExercises.push(exercise);
      }       
    });
  }

  addExerciseBlock(exercise:Exercise){
    this.searching = false;

    var newBlock:Block = {
      id: "",
      exercise: exercise,
      sets: 3,
      repetitions: 10
    };

    this.selectedWorkout?.blocks.push(newBlock);

    
    console.log(exercise.name);
  }

  deleteExerciseBlock(block:Block){
    var blockIndex = this.selectedWorkout?.blocks.findIndex(
      (currBlock) => currBlock === block
    ); 

    if(blockIndex == undefined) return;
    this.selectedWorkout?.blocks.splice(blockIndex, 1);
  }

  saveWorkoutChanges(){
    //this.http.p
    this.closeModal();
  }

  cancelWorkoutChanges(){
    this.http.get<Routine>('/api/routines/current').subscribe(
      (response) => {
        this.routine = response;
      }
    );
    this.closeModal();
  }

}
