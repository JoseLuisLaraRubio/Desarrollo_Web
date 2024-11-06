import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { HomeNavBarComponent } from "../../components/home-nav-bar/home-nav-bar.component";

@Component({
  selector: "app-login-page",
  standalone: true,
  imports: [HomeNavBarComponent],
  templateUrl: "./login-page.component.html",
  styleUrl: "./login-page.component.scss",
})
export class LoginPageComponent {
  constructor(private readonly _router: Router) {}

  public onLogin(): void {
    this._router.navigate(["/main-page"]);
  }
}
