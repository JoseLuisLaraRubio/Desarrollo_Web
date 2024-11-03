import { FormGroup } from "@angular/forms";

import { FormObject } from "./form-object";

export type FormObjectGroup<T> = FormGroup<FormObject<T>>;
