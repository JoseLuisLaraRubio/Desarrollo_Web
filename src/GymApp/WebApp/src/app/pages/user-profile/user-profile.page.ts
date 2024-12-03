import { Component, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { UserNavBarComponent } from "@components/user-nav-bar/user-nav-bar.component";

@Component({
  selector: "app-user-profile",
  templateUrl: "./user-profile.page.html",
  styleUrls: ["./user-profile.page.scss"],
  standalone: true,
  imports: [CommonModule, FormsModule, UserNavBarComponent],
})
export class UserProfilePage implements OnInit {
  user = {
    fullName: "",
    sex: "",
    dateOfBirth: "",
    height: null,
    weight: null,
    bodyType: "",
  };

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getPersonalInfo();
  }

  // Obtener la información personal del usuario
  getPersonalInfo() {
    const url = `api/personal-info`;
    this.http.get<any>(url).subscribe(
      (response) => {
        this.user = { ...this.user, ...response };
      },
      (error) => {
        console.error("Error al obtener la información personal:", error);
      },
    );
  }

  onSubmit() {
    if (
      !this.user.fullName ||
      !this.user.height ||
      !this.user.weight ||
      !this.user.sex ||
      !this.user.bodyType ||
      !this.user.dateOfBirth
    ) {
      alert("Por favor, llena todos los campos antes de guardar.");
      return;
    }

    const url = `api/personal-info`;
    this.http.put(url, this.user).subscribe(
      (response) => {
        alert("¡Tu perfil ha sido actualizado!");
        this.getPersonalInfo();
      },
      (error) => {
        console.error("Error al actualizar la información:", error);
        alert("Error al guardar los cambios.");
      },
    );
  }

  onCancel() {
    this.user = {
      fullName: "",
      sex: "",
      dateOfBirth: "",
      height: null,
      weight: null,
      bodyType: "",
    };
  }
}
