import { Component, Input } from "@angular/core";
import { Router, RouterLink } from "@angular/router";
import { AuthService } from "@services/auth";

@Component({
  selector: "app-user-nav-bar",
  standalone: true,
  imports: [RouterLink],
  templateUrl: "./user-nav-bar.component.html",
  styleUrl: "./user-nav-bar.component.scss",
})
export class UserNavBarComponent {
  constructor(
    private readonly _authService: AuthService,
    private readonly _router: Router,
  ) {}

  @Input() profile = false;
  @Input() routine = false;
  @Input() login = false;
  @Input() register = false;

  public onClickToExit(): void {
    this._authService.logout();
    this._router.navigate(["/auth/home"]);
  }

  public onClickToProfile(): void {
    this._router.navigate(["/user-profile"]);
  }
}
