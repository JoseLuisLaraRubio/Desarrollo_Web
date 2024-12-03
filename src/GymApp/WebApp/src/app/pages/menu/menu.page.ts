import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";
import { Nullable } from "@customTypes/nullable";
import { AuthService, UserInfoResponse } from "@services/auth";
import { Observable } from "rxjs";

@Component({
  selector: "app-menu",
  templateUrl: "./menu.page.html",
  styleUrls: ["./menu.page.scss"],
  standalone: true,
  imports: [CommonModule, UserNavBarComponent],
})
export class MenuPage {
  private readonly _userInfo: Observable<Nullable<UserInfoResponse>>;

  constructor(private readonly _authService: AuthService) {
    this._userInfo = _authService.getUserInfo();
  }

  public get userInfo() {
    return this._userInfo;
  }
}
