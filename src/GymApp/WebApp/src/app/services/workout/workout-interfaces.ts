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

export interface SubmitProgressRequest {
  date: string;
  results: ProgressResult[];
}

export interface ProgressResult {
  exerciseId: string;
  sets: ProgressSet[];
}

export interface ProgressSet {
  weight: number;
  repetitions: number;
}
