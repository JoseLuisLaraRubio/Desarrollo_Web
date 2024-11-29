import { ChangeDetectionStrategy, Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import {
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { HttpErrorResponse } from "@angular/common/http";

import { IonButton, IonContent } from "@ionic/angular/standalone";

import { AuthBasePage } from "../auth.base.page";

import { InputFieldComponent } from "@components/input-field";

import { RegisterRequest } from "@services/auth";

import { FormObject, FormObjectGroup } from "@customTypes/.";

import { PasswordFieldMatcher } from "./password-field-matcher";

import { environment } from "@environment";
import { NavBarComponent } from "@components/nav-bar/nav-bar.component";

interface AdminRegistrationForm extends RegisterRequest {
  passwordConfirm: string;
}

@Component({
  selector: "app-register",
  templateUrl: "./register.page.html",
  styleUrls: ["./register.page.scss"],
  standalone: true,
  imports: [
    NavBarComponent,
    InputFieldComponent,
    IonContent,
    IonButton,
    ReactiveFormsModule,
    RouterModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterPage extends AuthBasePage<AdminRegistrationForm> {
  private passwordFieldMatcher!: PasswordFieldMatcher;

  protected override createAuthForm(
    builder: NonNullableFormBuilder,
  ): FormObjectGroup<AdminRegistrationForm> {
    const passwordFieldMatcher = new PasswordFieldMatcher(
      "password",
      "passwordConfirm",
      (name) => document.getElementById(name)!.querySelector(".native-input")!,
    );

    this.passwordFieldMatcher = passwordFieldMatcher;

    const authForm = builder.group<FormObject<AdminRegistrationForm>>(
      {
        userName: builder.control("", [Validators.required]),
        password: builder.control("", [
          Validators.required,
          Validators.minLength(environment.passwordMinLength),
        ]),
        passwordConfirm: builder.control("", [Validators.required]),
      },
      {
        validators: passwordFieldMatcher.matchValidator(),
      },
    );

    authForm.statusChanges.subscribe(() => {
      this.updateMismatchPasswordState(authForm);
    });

    return authForm;
  }

  protected updateMismatchPasswordState(formGroup: FormGroup): void {
    const state = this.passwordFieldMatcher.getMismatchState(formGroup);
    this._errorMessage.set(state);
  }

  protected override onSubmitValue(value: AdminRegistrationForm): void {
    this._errorMessage.set(null);

    this._authService.registerUser(value).subscribe({
      next: () => {
        this._router.navigate(["../login"]);
      },
      error: (response: HttpErrorResponse) => {
        const errorMessageValue =
          this._errorMessageProvider.formatErrorResponse(response);

        this._errorMessage.set(errorMessageValue);
      },
    });
  }
}
