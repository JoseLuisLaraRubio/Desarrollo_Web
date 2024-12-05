import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { RoutineTracking } from '.';

@Injectable({
  providedIn: 'root',
})
export class RoutineTrackingService {
  constructor(private readonly _httpClient: HttpClient) {}

  public getRoutineTracking(workoutsId: number): Observable<RoutineTracking[]> {
    const apiPath: string = this.getApiPath(`/routines/workouts/${workoutsId}/progress`);
    return this._httpClient.get<RoutineTracking[]>(apiPath).pipe(
      catchError((error) => {
        console.error('Error fetching routines:', error);
        return of([]);
      })
    );
  }

  private getApiPath(path: string): string {
    return `api${path}`;
  }
}
