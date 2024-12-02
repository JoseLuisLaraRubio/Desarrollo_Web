import { Component, OnInit } from '@angular/core';
import { UserNavBarComponent } from "../../components/user-nav-bar/user-nav-bar.component";

@Component({
  selector: 'app-routine-overview',
  standalone: true,
  templateUrl: './routine-overview.component.html',
  styleUrls: ['./routine-overview.component.scss'],
  imports: [UserNavBarComponent],
})
export class RoutineOverviewComponent  implements OnInit {

  constructor() { }

  ngOnInit() {}

}
