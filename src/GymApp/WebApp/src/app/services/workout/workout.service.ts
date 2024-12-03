import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { SubmitProgressRequest, Workout } from './workout-interfaces';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  constructor(private readonly _httpClient: HttpClient) {}

   public getWorkoutById(workoutId: string): Observable<Workout> {
    const apiPath: string = this.getApiPath(`/routines/workouts/${workoutId}`);
    return this._httpClient.get<Workout>(apiPath).pipe(
      catchError((error) => {
        console.error('Error fetching workout:', error);
        return of({ id: '', blocks: [] });
      })
    );
  }

  public submitProgress(workoutId: string, progress: SubmitProgressRequest): Observable<void> {
    const apiPath: string = this.getApiPath(`/routines/workouts/${workoutId}/progress`);
    return this._httpClient.post<void>(apiPath, progress).pipe(
      catchError((error) => {
        console.error('Error submitting progress:', error);
        throw error;
      })
    );
  }

  private getApiPath(path: string): string {
    return `api${path}`;
  }
}
