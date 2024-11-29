import {
  ChangeDetectionStrategy,
  Component,
  input,
  output,
} from "@angular/core";
import {
  ControlContainer,
  FormGroupDirective,
  ReactiveFormsModule,
} from "@angular/forms";
import {
  IonInput,
  IonIcon,
  IonNote,
  IonInputPasswordToggle,
} from "@ionic/angular/standalone";

import { Nullable } from "@customTypes/nullable";

@Component({
  selector: "app-input-field",
  templateUrl: "./input-field.component.html",
  styleUrls: ["./input-field.component.scss"],
  standalone: true,
  imports: [
    IonInput,
    IonIcon,
    IonNote,
    ReactiveFormsModule,
    IonInputPasswordToggle,
  ],
  viewProviders: [
    {
      provide: ControlContainer,
      useExisting: FormGroupDirective,
    },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InputFieldComponent {
  public readonly _name = input("");
  public readonly _type = input("");
  public readonly _label = input("");
  public readonly _placeholder = input("");
  public readonly _icon = input("");
  public readonly _errorMessage = input<Nullable<string>>(null);
  public readonly _blur = output();
}
