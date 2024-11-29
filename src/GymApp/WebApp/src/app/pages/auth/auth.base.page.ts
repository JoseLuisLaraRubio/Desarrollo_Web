import { Directive, OnInit, signal } from "@angular/core";
import { Router } from "@angular/router";
import {
  AbstractControl,
  FormBuilder,
  NonNullableFormBuilder,
} from "@angular/forms";

import { addIcons } from "ionicons";
import { personOutline, lockClosedOutline, keyOutline } from "ionicons/icons";

import { AuthService } from "@services/auth";
import { ErrorMessageProviderService } from "@services/error-message-provider";

import { Nullable, FormObjectGroup } from "@customTypes/.";

@Directive()
export abstract class AuthBasePage<TValue> implements OnInit {
  protected readonly _authForm =
    signal<Nullable<FormObjectGroup<TValue>>>(null);

  protected readonly _errorMessage = signal<Nullable<string>>(null);

  constructor(
    protected readonly _router: Router,
    protected readonly _authService: AuthService,
    protected readonly _errorMessageProvider: ErrorMessageProviderService,
  ) {
    addIcons({ personOutline, lockClosedOutline, keyOutline });
  }

  ngOnInit(): void {
    const builder = new FormBuilder().nonNullable;
    this._authForm.set(this.createAuthForm(builder));
  }

  protected abstract createAuthForm(
    builder: NonNullableFormBuilder,
  ): FormObjectGroup<TValue>;

  protected getErrorMsg(controlName: string): Nullable<string> {
    const control: Nullable<AbstractControl> =
      this._authForm()!.get(controlName);

    if (!control) {
      throw new Error(`Control '${controlName}' not found.`);
    }

    if (!this.isControlInInvalidState(control)) {
      return null;
    }

    return "Error";
  }

  private isControlInInvalidState(control: AbstractControl): boolean {
    return control.invalid && control.touched;
  }

  protected onSubmit(): void {
    this.onSubmitValue(this._authForm()!.getRawValue() as TValue);
  }

  protected abstract onSubmitValue(value: TValue): void;
}
