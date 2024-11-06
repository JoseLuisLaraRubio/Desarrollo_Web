import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home-nav-bar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home-nav-bar.component.html',
  styleUrl: './home-nav-bar.component.scss'
})
export class HomeNavBarComponent {

}
