import { Component, Input } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "@services/auth";

@Component({
  selector: "app-user-nav-bar",
  standalone: true,
  imports: [],
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

  public onClickToMenu(): void {
    this._router.navigate(["/menu"]);
  }

  public onClickToExit(): void {
    this._authService.logout().subscribe({
      next: () => {
        console.log("Logout exitoso");
        this._router.navigate(["/auth/home"]).then(() => {
          window.location.reload();
        });
      },
      error: (err) => {
        console.error("Error durante el logout:", err);
      },
    });
  }

  public onClickToProfile(): void {
    this._router.navigate(["/user-profile"]);
  }

  public onClickToOverview(): void {
    this._router.navigate(["/overview"]);
  }
}
