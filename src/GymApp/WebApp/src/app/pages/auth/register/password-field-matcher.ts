import {
  AbstractControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
} from "@angular/forms";

import { Nullable } from "@customTypes/.";

export class PasswordFieldMatcher {
  public constructor(
    public readonly controlName: string,
    public readonly matchingControlName: string,
    private readonly inputElementSelector: (name: string) => Element,
  ) {}

  public matchValidator(): ValidatorFn {
    return (abstractControl: AbstractControl) => {
      const control = abstractControl.get(this.controlName);
      const matchingControl = abstractControl.get(this.matchingControlName);

      return !control?.value ||
        !matchingControl?.value ||
        control.value === matchingControl.value
        ? null
        : { mismatch: true };
    };
  }

  public getMismatchState(formGroup: FormGroup): Nullable<string> {
    const errors: Nullable<ValidationErrors> = formGroup.errors;
    if (!errors?.["mismatch"]) {
      return null;
    }

    const controlInput = this.inputElementSelector(this.controlName);
    const matchingControlInput = this.inputElementSelector(
      this.matchingControlName,
    );

    return controlInput === document.activeElement ||
      matchingControlInput === document.activeElement
      ? null
      : "Password mismatch";
  }
}
