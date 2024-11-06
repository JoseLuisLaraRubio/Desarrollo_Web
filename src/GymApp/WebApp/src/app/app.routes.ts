import { Routes } from "@angular/router";
import { HomePageComponent } from "./pages/home-page/home-page.component";
import { LoginPageComponent } from "./pages/login-page/login-page.component";
import { RegisterPageComponent } from "./pages/register-page/register-page.component";
import { MainPageComponent } from "./pages/main-page/main-page.component";
import { PlansPageComponent } from "./pages/plans-page/plans-page.component";
import { ProfilePageComponent } from "./pages/profile-page/profile-page.component";

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
    path: "main-page",
    component: MainPageComponent,
  },
  {
    path: "plans-page",
    component: PlansPageComponent,
  },
  {
    path: "profile",
    component: ProfilePageComponent,
  },
  {
    path: "**",
    redirectTo: "",
  },
];
