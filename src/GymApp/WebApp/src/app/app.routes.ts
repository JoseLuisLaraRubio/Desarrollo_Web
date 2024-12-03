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
    path: 'overview',
    loadComponent: () => import('./pages/routine-overview/routine-overview.page').then( m => m.RoutineOverviewPage)
  },  {
    path: 'quiz',
    loadComponent: () => import('./quiz/quiz.page').then( m => m.QuizPage)
  },



];
