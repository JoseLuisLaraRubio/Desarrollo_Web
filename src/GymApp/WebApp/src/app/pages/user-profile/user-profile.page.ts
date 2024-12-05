import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { BehaviorSubject, ReplaySubject, switchMap, tap } from "rxjs";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";

import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";
import { PersonalInfoService } from "@services/personal-info/personal-info.service";
import { PersonalInfo } from "@services/personal-info/data";
import { Nullable } from "@customTypes/nullable";
import { HttpClient } from "@angular/common/http";

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

  public preview: SafeUrl | null = this.sanitizer.bypassSecurityTrustUrl('assets/plans-page/default-avatar.png.jpg');
  // public files: any = [];
  constructor(private readonly personalInfoService: PersonalInfoService, private sanitizer: DomSanitizer,
    private httpClient : HttpClient
  ) {}

  public ngOnInit(): void {
    this.personalInfoService.getPersonalInfo().subscribe((info) => {
      this.personalInfo$.next(info ?? ({} as PersonalInfo));
    });
  }

  public onClickToPrint(): void {
    this.isPrinting = true;

    setTimeout(() => {
      window.print();
      this.isPrinting = false;
    }, 100);
  }

  public onSubmit(personalInfo: PersonalInfo): void {
    this.updateProfileImage();
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

  captureFile(event: any):any{
    const captureFile = event.target.files[0];
    // this.files.push(captureFile);
    this.extraerBase64(captureFile).then((imagen: any) => {
      console.log(imagen);
      this.preview = imagen.base;
    });
  };

  extraerBase64 = async ($event: any) => new Promise((resolve, reject) => {
    try {
      const unsafeImg = window.URL.createObjectURL($event);
      const image = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
      const reader = new FileReader();
      reader.readAsDataURL($event);
      reader.onload = () => {
        resolve({
          base: image
        });
      };
      reader.onerror = error => {
        resolve({
          base: null
        });
    }
    }
    catch (e) {
    reject(e);
    }

});

updateProfileImage(): any {
  const pictureFile = new FormData();
  if (typeof this.preview === 'string') {
    pictureFile.append('pictureFile', this.preview);
  } else {
    console.error('Preview is not a valid string');
  }

  this.httpClient.put('api/personal-info/picture', pictureFile).subscribe((res: any) => {
    console.log("Respuesta del servidor: ",res);
  });
}


}
