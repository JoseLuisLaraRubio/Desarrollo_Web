import { Component } from "@angular/core";
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: "app-nav-bar",
  standalone: true,
  imports: [RouterLink],
  templateUrl: "./nav-bar.component.html",
  styleUrl: "./nav-bar.component.scss",
})
export class NavBarComponent {
  constructor(private readonly router: Router) {}

  public onClickToLogin(): void {
    this.router.navigate(["/auth/login"]);
  }

  public onClickToRegister(): void {
    this.router.navigate(["/auth/register"]);
  }
}
