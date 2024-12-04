import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';
import { switchMap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { Guid } from '@customTypes/guid';

interface Routine {
  id: string;
  name: string;
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

interface RoutineUpdate{
  name: string,
  workouts:
  {
    id: string,
    blocks: 
      {
        id:string,
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

  constructor(private readonly http: HttpClient) {}

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
      id: this.generateGuid(),
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

  saveWorkoutChanges(){
    if (!this.routine || !this.selectedWorkout) {
      console.error("Routine or selected workout is undefined.");
      return;
    }
  
    // Map the routine data into the RoutineUpdate format
    const routineUpdate: RoutineUpdate = {
      name: this.routine.name,
      workouts: this.routine.workouts.map(workout => ({
        id: workout.id,
        blocks: workout.blocks.map(block => ({
          id: block.id,
          exerciseId: block.exercise.id,
          sets: block.sets,
          repetitions: block.repetitions,
        }))
      }))
    };

    console.log(routineUpdate)

    this.http.put('/api/routines/current', routineUpdate).subscribe(
      (response) => {
        console.log(response)
      }
    )

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

  generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (char) {
      const random = Math.random() * 16 | 0;
      const value = char === 'x' ? random : (random & 0x3 | 0x8);
      return value.toString(16);
    });
  }

}
