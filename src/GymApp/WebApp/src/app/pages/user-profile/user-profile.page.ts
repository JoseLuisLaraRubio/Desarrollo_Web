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

  constructor(private readonly _personalInfoService: PersonalInfoService) {}

  public ngOnInit(): void {
    this.getPersonalInfo();
  }

  public onSubmit(): void {
    if (this.isPersonalInfoValid()) {
      alert("Por favor, llena todos los campos antes de guardar.");
      return;
    }

    this.updatePersonalInfo(this.personalInfo);
    console.log(this.personalInfo)
    this.getPersonalInfo();
    console.log(this.personalInfo)
  }

  private updatePersonalInfo(personalInfo: PersonalInfo): void {
    this._personalInfoService.updatePersonalInfo(personalInfo);
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

  private getPersonalInfo(): void {
    const personalInfoObservable = this._personalInfoService.getPersonalInfo();

    personalInfoObservable.subscribe((info) => {
      this.personalInfo = { ...info };
    });
  }
}
