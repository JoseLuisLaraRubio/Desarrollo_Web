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
    loadComponent: () => import('./pages/menu/menu.page').then(m => m.MenuPage)
  },
  {
    path: 'user-profile',
    loadComponent: () => import('./pages/user-profile/user-profile.page').then(m => m.UserProfilePage)
  },
  {
    path: 'user-profile',
    loadComponent: () => import('./pages/user-profile/user-profile.page').then(m => m.UserProfilePage)
  },
  {
    path: 'workouts-log',
    loadComponent: () => import('./pages/workouts-log/workouts-log.page').then( m => m.WorkoutsLogPage)
  },
  {
    path: 'overview',
    loadComponent: () => import('./pages/routine-overview/routine-overview.page').then( m => m.RoutineOverviewPage)
  },
  {
    path: 'quiz',
    loadComponent: () => import('./pages/quiz/quiz.page').then( m => m.QuizPage)
  },


];
