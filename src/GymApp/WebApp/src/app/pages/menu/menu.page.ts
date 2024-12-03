import { Component, OnInit } from '@angular/core';
import { Navbar2Component } from "@components/navbar2/navbar2.component";


@Component({
  selector: 'app-menu',
  templateUrl: './menu.page.html',
  styleUrls: ['./menu.page.scss'],
  standalone: true,
  imports: [Navbar2Component],
})
export class MenuPage {
}
