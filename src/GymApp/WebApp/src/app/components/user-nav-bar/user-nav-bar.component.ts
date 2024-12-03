import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-nav-bar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-nav-bar.component.html',
  styleUrl: './user-nav-bar.component.scss'
})
export class UserNavBarComponent {
  @Input() logo = true;
  @Input() profile = false;
  @Input() routine = false;
  @Input() login = false;
  @Input() register = false;
}
