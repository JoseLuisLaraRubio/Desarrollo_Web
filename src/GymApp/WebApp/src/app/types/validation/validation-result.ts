import { ValidationError } from "./validation-error";

export class ValidationResult {
  private constructor(
    private readonly _succeeded: boolean = false,
    private readonly _errors: ReadonlyArray<ValidationError>,
  ) {}

  public get succeeded(): boolean {
    return this._succeeded;
  }

  public get errors(): ReadonlyArray<ValidationError> {
    return this._errors;
  }

  public static success() {
    return new ValidationResult(true, []);
  }

  public static failed(...errors: ValidationError[]): ValidationResult {
    if (errors == null) {
      throw new Error("Argument cannot be null or undefined.");
    }

    const failedResult = new ValidationResult(false, errors);
    return failedResult;
  }
}
