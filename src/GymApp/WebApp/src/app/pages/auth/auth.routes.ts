import { inject } from "@angular/core";
import { CanActivateFn, Router, Routes } from "@angular/router";

import { map } from "rxjs";

import { AuthService, UserInfoResponse } from "@services/auth";
import { Nullable } from "@customTypes/nullable";

function notLoggedIn(): CanActivateFn {
  return () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    return authService.getUserInfo().pipe(
      map((userInfo: Nullable<UserInfoResponse>) => {
        const loggedIn: boolean = userInfo != null;
        if (loggedIn) {
          router.navigate(["../menu"]);
        }

        return !loggedIn;
      }),
    );
  };
}

export const routes: Routes = [
  {
    path: "auth",
    children: [
      {
        path: "",
        redirectTo: "home",
        pathMatch: "full",
      },
      {
        path: "home",
        loadComponent: () => import("./home/home.page").then((m) => m.HomePage),
      },
      {
        path: "login",
        loadComponent: () =>
          import("./login/login.page").then((m) => m.LoginPage),
        canActivate: [notLoggedIn()],
      },
      {
        path: "register",
        loadComponent: () =>
          import("./register/register.page").then((m) => m.RegisterPage),
        canActivate: [notLoggedIn()],
      },
    ],
  },
  {
    path: "",
    redirectTo: "auth",
    pathMatch: "full",
  },
];
