import { Component } from "@angular/core";
import { range } from "../../utilities/random";
import { FormsModule } from "@angular/forms";
import { NavBarComponent } from "../../components/nav-bar/nav-bar.component";

@Component({
  selector: "app-register-page",
  standalone: true,
  imports: [FormsModule, NavBarComponent],
  templateUrl: "./register-page.component.html",
  styleUrl: "./register-page.component.scss",
})
export class RegisterPageComponent {
  private _progressValue: number;
  private _questionNumber: number;

  constructor() {
    this._progressValue = range(0, 30);
    this._questionNumber = 1;
  }

  public changeProgressValue(): void {
    this._progressValue = range(this._progressValue, 100);
  }

  public changeQuestionNumber(): void {
    this._questionNumber++;
  }

  public get progressValue() {
    return this._progressValue;
  }

  public get questionNumber() {
    return this._questionNumber;
  }
}
