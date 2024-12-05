import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";
import { PersonalInfoService } from "@services/personal-info/personal-info.service";
import { PersonalInfo } from "@services/personal-info/data";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.page.html",
  styleUrls: ["./user-profile.page.scss"],
  standalone: true,
  imports: [CommonModule, FormsModule, UserNavBarComponent],
})
export class UserProfilePage implements OnInit {
  personalInfo: PersonalInfo = {} as PersonalInfo;
  isPrinting: boolean = false; // Nuevo estado para manejar el formato de impresión

  constructor(private readonly _personalInfoService: PersonalInfoService) {}

  public ngOnInit(): void {
    this.getPersonalInfo();
  }

  public onClickToPrint(): void {
    this.isPrinting = true;
    this.getPersonalInfo();
    setTimeout(() => {
      window.print();
      this.isPrinting = false;
    }, 100);
  }

  public onSubmit(): void {
    if (
      !this.isPersonalInfoValid() &&
      this.isHeightValid() &&
      this.isWeightValid()
    ) {
      this.updatePersonalInfo(this.personalInfo);
      this.getPersonalInfo();
      return;
    }
    alert("Por favor, llena todos los campos correctamente antes de guardar.");
  }

  private updatePersonalInfo(personalInfo: PersonalInfo): void {
    this._personalInfoService.updatePersonalInfo(personalInfo).subscribe();
  }

  private isPersonalInfoValid(): boolean {
    return (
      !this.personalInfo.fullName ||
      !this.personalInfo.height ||
      !this.personalInfo.weight ||
      !this.personalInfo.sex ||
      !this.personalInfo.bodyType ||
      !this.personalInfo.dateOfBirth
    );
  }

  private isHeightValid(): boolean {
    return this.personalInfo.height > 0;
  }

  private isWeightValid(): boolean {
    return this.personalInfo.weight > 0;
  }

  private getPersonalInfo(): void {
    this._personalInfoService.getPersonalInfo().subscribe((info) => {
      this.personalInfo = { ...info };
    });
  }
}
