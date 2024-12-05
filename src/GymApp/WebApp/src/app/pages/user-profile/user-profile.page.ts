import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { BehaviorSubject, ReplaySubject, switchMap, tap } from "rxjs";

import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";
import { PersonalInfoService } from "@services/personal-info/personal-info.service";
import { PersonalInfo } from "@services/personal-info/data";
import { Nullable } from "@customTypes/nullable";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.page.html",
  styleUrls: ["./user-profile.page.scss"],
  standalone: true,
  imports: [CommonModule, FormsModule, UserNavBarComponent],
})
export class UserProfilePage implements OnInit {
  personalInfo$ = new ReplaySubject<PersonalInfo>();

  isPrinting: boolean = false;

  constructor(private readonly _personalInfoService: PersonalInfoService) {}

  public ngOnInit(): void {
    this.personalInfoService.getPersonalInfo().subscribe((info) => {
      this.personalInfo$.next(info ?? ({} as PersonalInfo));
    });
  }

  public onClickToPrint(): void {
    this.isPrinting = true;
    // Llama a la función de impresión del navegador
    setTimeout(() => {
      window.print();
      this.isPrinting = false;
    }, 100); // Da tiempo a Angular para aplicar cambios en el DOM
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

  private isPersonalInfoValid(personalInfo: PersonalInfo): boolean {
    return (
      !personalInfo.fullName ||
      !personalInfo.height ||
      !personalInfo.weight ||
      !personalInfo.sex ||
      !personalInfo.bodyType ||
      !personalInfo.dateOfBirth
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
