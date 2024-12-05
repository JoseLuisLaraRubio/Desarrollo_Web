import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { catchError, map, of, ReplaySubject } from "rxjs";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";

import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";
import { PersonalInfoService } from "@services/personal-info/personal-info.service";
import { PersonalInfo } from "@services/personal-info/data";
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
  public preview: SafeUrl | null = this.sanitizer.bypassSecurityTrustUrl(
    "assets/plans-page/default-avatar.png.jpg",
  );
  public files: File[] = []; // Declare the files array to store the uploaded files

  constructor(
    private readonly personalInfoService: PersonalInfoService,
    private readonly sanitizer: DomSanitizer,
    private readonly httpClient: HttpClient,
  ) {}

  ngOnInit(): void {
    this.personalInfoService.getPersonalInfo().subscribe((info) => {
      this.personalInfo$.next(info ?? ({} as PersonalInfo));

      this.httpClient
        .get("http://localhost:53722/api/personal-info/picture", {
          responseType: "blob",
        })
        .pipe(
          map((imageBlob: Blob) => {
            const imageUrl = URL.createObjectURL(imageBlob);
            this.preview = this.sanitizer.bypassSecurityTrustUrl(imageUrl);
          }),
          catchError((error) => {
            console.error("Error al cargar la imagen: ", error);
            return of(null);
          }),
        )
        .subscribe();
    });
  }

  onClickToPrint(): void {
    this.isPrinting = true;

    setTimeout(() => {
      window.print();
      this.isPrinting = false;
    }, 100);
  }

  onSubmit(personalInfo: PersonalInfo): void {
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

  captureFile(event: any): void {
    const captureFile = event.target.files[0];
    if (captureFile) {
      this.files.push(captureFile);
      this.extraerBase64(captureFile).then((imagen: any) => {
        this.preview = imagen.base;
      });
    }
  }

  extraerBase64 = async ($event: any) =>
    new Promise((resolve, reject) => {
      try {
        const unsafeImg = window.URL.createObjectURL($event);
        const image = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
        const reader = new FileReader();
        reader.readAsDataURL($event);
        reader.onload = () => {
          resolve({
            base: image,
          });
        };
        reader.onerror = (error) => {
          resolve({
            base: null,
          });
        };
      } catch (e) {
        reject(e);
      }
    });

  updateProfileImage(): void {
    const pictureFile = new FormData();
    if (this.files.length > 0) {
      pictureFile.append("pictureFile", this.files[0]);
    } else {
      console.error("No valid file to upload");
    }

    this.httpClient
      .put("api/personal-info/picture", pictureFile)
      .subscribe((res: any) => {
        console.log("Respuesta del servidor: ", res);
      });
  }
}
