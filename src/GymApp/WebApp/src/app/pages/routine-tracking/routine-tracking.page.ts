import { Component, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routine, RoutineTrackingService } from '@services/routine-tracking';
import { NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { Color } from '@swimlane/ngx-charts';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';

@Component({
  selector: 'app-routine-tracking',
  templateUrl: './routine-tracking.page.html',
  styleUrls: ['./routine-tracking.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, NgxChartsModule , UserNavBarComponent],
})
export class RoutineTrackingPage implements OnInit {
  routine: Routine | undefined;
  repetitionsData: any[] = [];
  weightData: any[] = [];
  view: [number, number] = [0, 0];

  constructor(private routineService: RoutineTrackingService) {}

  colorSchemeReps: Color = {
    name: 'custom-red',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#FF9999', '#FF6666', '#FF3333', '#FF0000', '#CC0000'],
  };

  colorSchemeWeight: Color = {
    name: 'custom-blue',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#CCE5FF', '#99CCFF', '#66B2FF', '#3399FF', '#007FFF', '#0059B2', '#003D80'],
  };

  ngOnInit() {
    this.fetchRoutineDataJson();
    this.updateChartSize();
  }

  fetchRoutineDataJson() {
    this.routine = {
      id: '1',
      name: 'Weekly Routine',
      workouts: [
        {
          id: 'w1',
          blocks: [
            {
              id: 'b1',
              exercise: {
                id: 'e1',
                name: 'Push Up',
                primaryMuscle: 'Chest',
                secondaryMuscles: ['Shoulders'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 15,
            },
            {
              id: 'b2',
              exercise: {
                id: 'e2',
                name: 'Pull Up',
                primaryMuscle: 'Back',
                secondaryMuscles: ['Biceps'],
                equipmentRequirement: 'Bar',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 4,
              repetitions: 10,
            },
          ],
        },
        {
          id: 'w2',
          blocks: [
            {
              id: 'b3',
              exercise: {
                id: 'e3',
                name: 'Squat',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Core'],
                equipmentRequirement: 'None',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 3,
              repetitions: 12,
            },
            {
              id: 'b4',
              exercise: {
                id: 'e4',
                name: 'Lunge',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Glutes'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 12,
            },
          ],
        },
        {
          id: 'w1',
          blocks: [
            {
              id: 'b1',
              exercise: {
                id: 'e1',
                name: 'Push Up',
                primaryMuscle: 'Chest',
                secondaryMuscles: ['Shoulders'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 15,
            },
            {
              id: 'b2',
              exercise: {
                id: 'e2',
                name: 'Pull Up',
                primaryMuscle: 'Back',
                secondaryMuscles: ['Biceps'],
                equipmentRequirement: 'Bar',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 4,
              repetitions: 10,
            },
          ],
        },
        {
          id: 'w2',
          blocks: [
            {
              id: 'b3',
              exercise: {
                id: 'e3',
                name: 'Squat',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Core'],
                equipmentRequirement: 'None',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 3,
              repetitions: 12,
            },
            {
              id: 'b4',
              exercise: {
                id: 'e4',
                name: 'Lunge',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Glutes'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 12,
            },
          ],
        },
        {
          id: 'w1',
          blocks: [
            {
              id: 'b1',
              exercise: {
                id: 'e1',
                name: 'Push Up',
                primaryMuscle: 'Chest',
                secondaryMuscles: ['Shoulders'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 15,
            },
            {
              id: 'b2',
              exercise: {
                id: 'e2',
                name: 'Pull Up',
                primaryMuscle: 'Back',
                secondaryMuscles: ['Biceps'],
                equipmentRequirement: 'Bar',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 4,
              repetitions: 10,
            },
          ],
        },
        {
          id: 'w2',
          blocks: [
            {
              id: 'b3',
              exercise: {
                id: 'e3',
                name: 'Squat',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Core'],
                equipmentRequirement: 'None',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 3,
              repetitions: 12,
            },
            {
              id: 'b4',
              exercise: {
                id: 'e4',
                name: 'Lunge',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Glutes'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 12,
            },
          ],
        },
        {
          id: 'w1',
          blocks: [
            {
              id: 'b1',
              exercise: {
                id: 'e1',
                name: 'Push Up',
                primaryMuscle: 'Chest',
                secondaryMuscles: ['Shoulders'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 15,
            },
            {
              id: 'b2',
              exercise: {
                id: 'e2',
                name: 'Pull Up',
                primaryMuscle: 'Back',
                secondaryMuscles: ['Biceps'],
                equipmentRequirement: 'Bar',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 4,
              repetitions: 10,
            },
          ],
        },
        {
          id: 'w2',
          blocks: [
            {
              id: 'b3',
              exercise: {
                id: 'e3',
                name: 'Squat',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Core'],
                equipmentRequirement: 'None',
                difficultyLevel: 3,
                effectivenessLevel: 5,
              },
              sets: 3,
              repetitions: 12,
            },
            {
              id: 'b4',
              exercise: {
                id: 'e4',
                name: 'Lunge',
                primaryMuscle: 'Legs',
                secondaryMuscles: ['Glutes'],
                equipmentRequirement: 'None',
                difficultyLevel: 2,
                effectivenessLevel: 4,
              },
              sets: 3,
              repetitions: 12,
            },
          ],
        },
      ],
    };
    this.generateChartData();
  }

  fetchRoutineData() {
    this.routineService.getCurrentRoutine().subscribe({
      next: (data) => {
        this.routine = data;
        this.generateChartData();
      },
      error: (err) => {
        console.error('Error fetching routine:', err);
      },
    });
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.updateChartSize();
  }

  updateChartSize() {
    const width = window.innerWidth * 0.9;
    const height = window.innerHeight * 0.65;
    this.view = [width, height];
  }

  generateChartData() {
    if (this.routine?.workouts) {
      const repetitions: any[] = [];
      const weight: any[] = [];

      this.routine.workouts.forEach((workout, workoutIndex) => {
        workout.blocks.forEach((block, blockIndex) => {
          const uniqueName = `${block.exercise.name} (W${workoutIndex + 1}B${blockIndex + 1})`;

          repetitions.push({
            name: uniqueName,
            value: block.repetitions,
          });

          weight.push({
            name: uniqueName,
            value: Math.floor(Math.random() * 50) + 10,
          });
        });
      });

      console.log('Repetitions Data:', repetitions);
      console.log('Weight Data:', weight);

      this.repetitionsData = repetitions;
      this.weightData = weight;
    }
  }
}
