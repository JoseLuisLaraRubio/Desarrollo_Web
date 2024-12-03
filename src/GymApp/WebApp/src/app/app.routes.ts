import { Routes } from "@angular/router";

export const routes: Routes = [
  {
    path: "",
    loadChildren: () =>
      import("./pages/auth/auth.routes").then((m) => m.routes),
  },
  {
    path: "",
    redirectTo: "",
    pathMatch: "full",
  },
  {
    path: 'menu',
    loadComponent: () => import('./pages/menu/menu.page').then( m => m.MenuPage)
  },
  {
    path: 'workouts-log',
    loadComponent: () => import('./pages/workouts-log/workouts-log.page').then( m => m.WorkoutsLogPage)
  },

];
