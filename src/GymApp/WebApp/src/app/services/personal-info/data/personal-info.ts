import { BodyType } from "./body-type";
import { Sex } from "./sex";

export interface PersonalInfo {
  fullName: string;
  sex: Sex;
  dateOfBirth: string;
  height: number;
  weight: number;
  bodyType: BodyType;
}
