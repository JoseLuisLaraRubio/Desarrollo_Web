import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { NavBarComponent } from "@components/nav-bar/nav-bar.component";

@Component({
  selector: "app-home-page",
  standalone: true,
  imports: [NavBarComponent],
  templateUrl: "./home.page.html",
  styleUrl: "./home.page.scss",
})
export class HomePage {
  constructor(private readonly _router: Router) {}

  public onClickToLogin(): void {
    this._router.navigate(["/auth/login"]);
  }
}
