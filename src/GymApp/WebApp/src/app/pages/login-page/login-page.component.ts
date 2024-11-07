import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavBarComponent } from "../../components/nav-bar/nav-bar.component";

@Component({
  selector: "app-login-page",
  standalone: true,
  imports: [NavBarComponent],
  templateUrl: "./login-page.component.html",
  styleUrl: "./login-page.component.scss",
})
export class LoginPageComponent {
  constructor(private readonly _router: Router) {}

  public onLogin(): void {
    this._router.navigate(["/profile"]);
  }
}
