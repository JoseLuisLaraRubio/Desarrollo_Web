import { FormControl } from "@angular/forms";

export type FormObject<T> = {
  [K in keyof T]: FormControl<T[K]>;
};
