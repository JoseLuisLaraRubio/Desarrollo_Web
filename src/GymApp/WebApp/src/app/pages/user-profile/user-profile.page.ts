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

  constructor(private readonly personalInfoService: PersonalInfoService) {}

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

  public onSubmit(personalInfo: PersonalInfo): void {
    if (!this.isPersonalInfoValid(personalInfo)) {
      alert("Por favor, llena todos los campos antes de guardar.");
      return;
    }

    this.personalInfoService.updatePersonalInfo(personalInfo).subscribe();
  }

  private isPersonalInfoValid(personalInfo: PersonalInfo): boolean {
    return (
      !!personalInfo.fullName &&
      !!personalInfo.height &&
      !!personalInfo.weight &&
      !!personalInfo.sex &&
      !!personalInfo.bodyType &&
      !!personalInfo.dateOfBirth &&
      personalInfo.height > 0 &&
      personalInfo.weight > 0
    );
  }
}
