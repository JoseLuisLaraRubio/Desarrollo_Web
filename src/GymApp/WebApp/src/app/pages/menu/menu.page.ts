import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.page.html",
  styleUrls: ["./menu.page.scss"],
  standalone: true,
  imports: [CommonModule, UserNavBarComponent],
})
export class MenuPage {
  constructor(private readonly router: Router) {}

  public onClickToProfile(): void {
    this.router.navigate(["/user-profile"]);
  }

  public onClickToRoutine(): void {
    this.router.navigate(["/overview"]);
  }
}
