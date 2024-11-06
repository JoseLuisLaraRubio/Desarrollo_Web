import { Component } from "@angular/core";
import { NavBarComponent } from "../../components/nav-bar/nav-bar.component";
import { FormsModule } from "@angular/forms";

@Component({
  selector: "app-profile-page",
  standalone: true,
  imports: [NavBarComponent, FormsModule],
  templateUrl: "./profile-page.component.html",
  styleUrls: ["./profile-page.component.scss"],
})
export class ProfilePageComponent {
  private _accountName: string;
  private _email: string;
  private _password: string;
  public newAccountName: string;
  public newEmail: string;
  public newPassword: string;

  constructor() {
    // Valores iniciales
    this._accountName = "ElMasMamado777";
    this._email = "dedefr@yahoo.com";
    this._password = "12345";

    // Valores temporales de los inputs
    this.newAccountName = "";
    this.newEmail = "";
    this.newPassword = "";
  }

  public updatePlaceholders(): void {
    this._accountName = this.newAccountName;
    this._email = this.newEmail;
    this._password = this.newPassword;
    this.resetInputValues();
  }

  private resetInputValues(): void {
    this.newAccountName = "";
    this.newEmail = "";
    this.newPassword = "";
  }

  public get accountName(): string {
    return this._accountName;
  }

  public get email(): string {
    return this._email;
  }

  public get password(): string {
    return this._password;
  }
}
