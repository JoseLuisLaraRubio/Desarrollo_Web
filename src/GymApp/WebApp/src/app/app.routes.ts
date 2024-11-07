import { Routes } from "@angular/router";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { LoginPageComponent } from "./pages/login-page/login-page.component";
import { RegisterPageComponent } from "./pages/register-page/register-page.component";
import { PlansPageComponent } from "./pages/plans-page/plans-page.component";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";
import { RoutinePageComponent } from "./pages/routine-page/routine-page.component";
import { DayRoutinePageComponent } from "./pages/day-routine-page/day-routine-page.component";

export const routes: Routes = [
  {
    path: "",
    component: HomePageComponent,
  },
  {
    path: "login",
    component: LoginPageComponent,
  },
  {
    path: "register",
    component: RegisterPageComponent,
  },
  {
    path: "profile",
    component: ProfilePageComponent,
  },
  {
    path: "routine",
    component: RoutinePageComponent,
  },
  {
    path: "day-routine",
    component: DayRoutinePageComponent,
  },
  {
    path: "**",
    redirectTo: "",
  },
];
