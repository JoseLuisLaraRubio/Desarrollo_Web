import { ChangeDetectionStrategy, Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import {
  Validators,
  ReactiveFormsModule,
  NonNullableFormBuilder,
} from "@angular/forms";
import { HttpErrorResponse } from "@angular/common/http";

import { IonButton, IonContent } from "@ionic/angular/standalone";

import { InputFieldComponent } from "@components/input-field";

import { LoginRequest } from "@services/auth";

import { AuthBasePage } from "../auth.base.page";

import { FormObjectGroup } from "@customTypes/.";
import { NavBarComponent } from "@components/nav-bar/nav-bar.component";

@Component({
  selector: "app-login",
  templateUrl: "./login.page.html",
  styleUrls: ["./login.page.scss"],
  standalone: true,
  imports: [
    InputFieldComponent,
    NavBarComponent,
    IonContent,
    IonButton,
    ReactiveFormsModule,
    RouterModule,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginPage extends AuthBasePage<LoginRequest> {
  protected override createAuthForm(
    builder: NonNullableFormBuilder,
  ): FormObjectGroup<LoginRequest> {
    return builder.group({
      userName: builder.control("", [Validators.required]),
      password: builder.control("", [Validators.required]),
    });
  }

  protected override onSubmitValue(value: LoginRequest): void {
    this._errorMessage.set(null);

    this._authService.login(value).subscribe({
      next: () => {
        this._router.navigateByUrl("menu");
      },
      error: (response: HttpErrorResponse) => {
        const errorMessageValue =
          this._errorMessageProvider.formatErrorResponse(response);

        this._errorMessage.set(errorMessageValue);
      },
    });
  }
}
