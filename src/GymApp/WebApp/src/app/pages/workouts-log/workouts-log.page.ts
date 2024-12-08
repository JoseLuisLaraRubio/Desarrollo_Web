import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WorkoutService } from '@services/workout';
import { ActivatedRoute } from '@angular/router';
import { SubmitProgressRequest } from '@services/workout';
import { Workout } from '@services/workout';
import { UserNavBarComponent } from '@components/user-nav-bar/user-nav-bar.component';

@Component({
  selector: 'app-workouts-log',
  templateUrl: './workouts-log.page.html',
  styleUrls: ['./workouts-log.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule ,UserNavBarComponent ]
})
export class WorkoutsLogPage implements OnInit {
  workout: Workout | undefined;
  isModalOpen = false;
  selectedBlock: any = null;
  workoutId: string | null = null;
  progressData = {
    weight: 0,
    repetitions: 0
  };

  constructor(private workoutService: WorkoutService ,private route: ActivatedRoute) { }

  ngOnInit() {
    this.workoutId = this.route.snapshot.paramMap.get('workoutId');

    if (this.workoutId) {
      this.getDataWorkout();
    } else {
      console.error('No workoutId found in URL');
    }
  }

  getDataWorkout() {
    if (this.workoutId) {
      this.workoutService.getWorkoutById(this.workoutId).subscribe({
        next: (workout) => {
          this.workout = workout;
        },
        error: (err) => {
          console.error('Error fetching workout:', err);
          this.workout = { id: '', blocks: [] };
        }
      });
    }
  }

  openProgressModal(block: any) {
    this.selectedBlock = block;
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.progressData = { weight: 0, repetitions: 0 };
  }

  submitProgress() {
    const progress: SubmitProgressRequest = {
      date: new Date().toISOString(),
      results: [
        {
          exerciseId: this.selectedBlock.exercise.id,
          sets: [
            {
              weight: this.progressData.weight,
              repetitions: this.progressData.repetitions
            }
          ]
        }
      ]
    };

    this.workoutService.submitProgress(this.workout?.id || '', progress).subscribe({
      next: () => {
        console.log('Progress submitted successfully');
        this.closeModal();
      },
      error: (err) => {
        console.error('Error submitting progress:', err);
      }
    });
  }
}
