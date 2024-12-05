import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';
import { switchMap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Guid } from '@customTypes/guid';
import { Router, RouterLink } from '@angular/router';

interface Routine {
  id: string;
  name: string;
  workouts: Workout[];
}

interface Workout {
  id: string|null;
  blocks: Block[];
}

interface Block {
  id: string | null;
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

interface RoutineUpdate{
  name: string,
  workouts:
  {
    id: string|null,
    blocks: 
      {
        id:string|null,
        exerciseId:string,
        sets: number,
        repetitions: number
      }[]
  }[]
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

  constructor(private readonly http: HttpClient, private readonly _router: Router) {}

  ngOnInit()
  {
    this.http.get<Routine>('/api/routines/current').subscribe(
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
      id: null,
      exercise: exercise,
      sets: 3,
      repetitions: 10,
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

  saveRoutineChanges(){
    if (!this.routine) {
      console.log("Routine or is undefined.");
      return;
    }
  
    if (this.selectedWorkout != undefined) {
      for (const block of this.selectedWorkout.blocks) {
        if (block.repetitions <= 0 || block.sets <= 0) {
          alert("El número de sets y repeticiones no puede ser menor a 1");
          return;
        }
      }
    }

    // Map the routine data into the RoutineUpdate format
    const routineUpdate: RoutineUpdate = {
      name: this.routine.name,
      workouts: this.routine.workouts.map(workout => ({
        id: workout.id,
        blocks: workout.blocks.map(block => ({
          id: null,
          exerciseId: block.exercise.id,
          sets: block.sets,
          repetitions: block.repetitions,
        }))
      }))
    };

    this.http.put('/api/routines/current', routineUpdate).subscribe(
      (response) => {
        this.getStoredRoutine();
      }
    )

    this.closeModal();
  }

  cancelWorkoutChanges(){
    if(this.routine == undefined) return;

    this.getStoredRoutine();
    this.closeModal();
  }

  deleteWorkout(){
    var workoutIndex = this.routine?.workouts.findIndex(
      (workouts) => workouts === this.selectedWorkout
    ); 

    if(workoutIndex == undefined) return;
    this.routine?.workouts.splice(workoutIndex, 1);
    this.closeModal();
    this.saveRoutineChanges();
  }

  addDay(){
    if(this.routine == undefined || this.routine.workouts.length >= 7) return;

    var emptyWorkout:Workout = {
      id: null,
      blocks: []
    }

    this.routine?.workouts.push(emptyWorkout);
    this.saveRoutineChanges();
  }

  getStoredRoutine(){
    this.http.get<Routine>('/api/routines/current').subscribe(
      (response) => {
        this.routine = response;
      }
    );
  }

  startRoutine(workout:Workout){
    if (workout == null) return;
    this._router.navigate(['/workouts-log',{  workoutId: workout.id}]);
  }

}
