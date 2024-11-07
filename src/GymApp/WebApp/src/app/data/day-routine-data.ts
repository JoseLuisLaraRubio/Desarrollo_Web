import { DayRoutine } from "../types";
import { exerciseSets } from "./exercise-set-data";

export const weeklyRoutine: DayRoutine[] = [
  {
    dayNumber: 1,
    sets: [exerciseSets[0], exerciseSets[1], exerciseSets[2]],
  },
  {
    dayNumber: 2,
    sets: [exerciseSets[3], exerciseSets[4]],
  },
  {
    dayNumber: 3,
    sets: [exerciseSets[1], exerciseSets[2], exerciseSets[0]],
  },
  {
    dayNumber: 4,
    sets: [exerciseSets[1], exerciseSets[2], exerciseSets[0]],
  },
];
