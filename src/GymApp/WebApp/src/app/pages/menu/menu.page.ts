import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from "@components/nav-bar/nav-bar.component";


@Component({
  selector: 'app-menu',
  templateUrl: './menu.page.html',
  styleUrls: ['./menu.page.scss'],
  standalone: true,
  imports: [NavBarComponent],
})
export class MenuPage {
}
