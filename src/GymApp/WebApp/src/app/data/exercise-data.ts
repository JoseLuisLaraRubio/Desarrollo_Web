import { Guid } from "../types/general";
import { Exercise } from "../types";

function createGuid(): Guid {
  return crypto.randomUUID() as Guid; // Utiliza la API de UUID del navegador y lo convierte al tipo Guid
}

export const exerciseData: Exercise[] = [
  {
    id: createGuid(),
    name: "Push-up",
    description:
      "Ejercicio de peso corporal para fortalecer el pecho y los brazos.",
  },
  {
    id: createGuid(),
    name: "Squat",
    description:
      "Ejercicio de peso corporal que trabaja las piernas y los glúteos.",
  },
  {
    id: createGuid(),
    name: "Plank",
    description: "Ejercicio isométrico para fortalecer el core y la espalda.",
  },
  {
    id: createGuid(),
    name: "Lunges",
    description:
      "Ejercicio que fortalece piernas y equilibrio, ideal para las piernas y glúteos.",
  },
  {
    id: createGuid(),
    name: "Bicep Curl",
    description: "Ejercicio con pesas para fortalecer los bíceps.",
  },
];
