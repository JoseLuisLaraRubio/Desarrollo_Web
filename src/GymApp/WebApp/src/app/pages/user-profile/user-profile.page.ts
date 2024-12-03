import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonContent, IonHeader, IonTitle, IonToolbar } from '@ionic/angular/standalone';
import { NavBarComponent } from "@components/nav-bar/nav-bar.component";
import { Navbar2Component } from "@components/navbar2/navbar2.component";

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.page.html',
  styleUrls: ['./user-profile.page.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule, Navbar2Component]
})
export class UserProfilePage {
  user = {
    username: 'gymlover123',
    fullName: '',
    age: null,
    height: null,
    sex: '',
    weight: null,
    bodyType: '',
  };

  onSubmit() {
    // Aquí podrías enviar la información actualizada al backend
    console.log('Información del usuario:', this.user);
    alert('¡Tu perfil ha sido actualizado!');
  }
}
