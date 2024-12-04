export interface Routine {
  id: string;
  name: string;
  workouts: Workout[];
}

export interface Workout {
  id: string;
  blocks: Block[];
}

export interface Block {
  id: string;
  exercise: Exercise;
  sets: number;
  repetitions: number;
}

export interface Exercise {
  id: string;
  name: string;
  primaryMuscle: string;
  secondaryMuscles: string[];
  equipmentRequirement: string;
  difficultyLevel: number;
  effectivenessLevel: number;
}
