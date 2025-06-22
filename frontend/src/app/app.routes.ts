import { Routes } from '@angular/router';
import { GiftProgramComponent } from './modules/candidate/pages/gift-program/gift-program.component';
import { UserSearchComponent } from './modules/candidate/pages/user-search/user-search.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { authGuard } from './shared/helpers/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/candidate' },
  {
    path: 'candidate',
    loadChildren: () =>
      import('./modules/candidate/candidate.routes').then(
        (m) => m.CANDIDATE_PATH
      ),
    canActivate: [authGuard],
    data: {
      role: ['Candidate'],
    },
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth.routes').then((m) => m.AUTH_ROUTES),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./modules/admin/dashboard/dashboard.routes').then(
        (m) => m.dashboard_routes
      ),
    canActivate: [authGuard],
    data: { role: ['Admin'] },
  },
  {
    path: 'management',
    loadChildren: () =>
      import('./modules/management/management.routes').then(
        (m) => m.MANAGEMENT_ROUTES
      ),
    canActivate: [authGuard],
    data: {
      role: ['HR', 'RMG', 'Manager'],
    },
  },
  {
    path: 'gift-program',
    component: GiftProgramComponent,
    canActivate: [authGuard],
  },
  {
    path: 'search',
    component: UserSearchComponent,
    canActivate: [authGuard],
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];
