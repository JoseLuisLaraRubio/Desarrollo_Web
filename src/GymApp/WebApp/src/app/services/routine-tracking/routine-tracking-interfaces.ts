export interface RoutineTracking {
  date: string;
  results: Result[];
}

export interface Result {
  exercise: ExerciseRoutine;
  sets: Set[];
}

export interface ExerciseRoutine {
  id: string;
  name: string;
}

export interface Set {
  weight: number;
  repetitions: number;
}
