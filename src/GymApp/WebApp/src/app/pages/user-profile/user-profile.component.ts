import { Component } from '@angular/core';
import { NavBarComponent } from '@components/nav-bar/nav-bar.component';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
  standalone: true,
  imports: [NavBarComponent],
})
export class UserProfileComponent {
  user = {
    username: 'gymlover123',
    fullName: '',
    age: null,
    height: null,
    weight: null,
    bodyType: '',
    favoriteSports: '',
  };

  onSubmit() {
    // Aquí podrías enviar la información actualizada al backend
    console.log('Información del usuario:', this.user);
    alert('¡Tu perfil ha sido actualizado!');
  }
}
