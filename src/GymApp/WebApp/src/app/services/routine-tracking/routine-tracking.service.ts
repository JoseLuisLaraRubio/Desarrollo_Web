import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Routine } from '.';

@Injectable({
  providedIn: 'root',
})
export class RoutineTrackingService {
  constructor(private readonly _httpClient: HttpClient) {}

  public getCurrentRoutine(): Observable<Routine> {
    const apiPath: string = this.getApiPath('/api/routines/current');
    return this._httpClient.get<Routine>(apiPath).pipe(
      catchError((error) => {
        console.error('Error fetching routine:', error);
        return of({ id: '', name: '', workouts: [] });
      })
    );
  }

  private getApiPath(path: string): string {
    return `api${path}`;
  }
}
