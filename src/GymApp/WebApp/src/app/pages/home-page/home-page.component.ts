import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { HomeNavBarComponent } from '../../components/home-nav-bar/home-nav-bar.component';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [RouterLink, HomeNavBarComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
