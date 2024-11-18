import { Component, OnInit } from '@angular/core';
import { PlansListComponent } from '../../components/plans-list/plans-list.component';
import { NavBarComponent } from "../../components/nav-bar/nav-bar.component";

@Component({
  selector: 'app-plans-page',
  standalone: true,
  imports: [PlansListComponent, NavBarComponent],
  templateUrl: './plans-page.component.html',
  styleUrl: './plans-page.component.scss'
})
export class PlansPageComponent implements OnInit{
  categories!: string[];

  ngOnInit(): void {
      this.categories = Array(3).fill("Categoria de prueba")
  }
}