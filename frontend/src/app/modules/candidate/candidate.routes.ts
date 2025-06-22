import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { authGuard } from '../../shared/helpers/auth.guard';
import { GiftProgramComponent } from './pages/gift-program/gift-program.component';
import { HomeComponent } from './pages/home/home.component';
import { ProfileViewComponent } from './pages/profile-view/profile-view.component';
import { RequestComponent } from './pages/request/request.component';
import { UserSearchComponent } from './pages/user-search/user-search.component';

export const CANDIDATE_PATH: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      {
        path: 'home',
        component: HomeComponent,
      },
      {
        path: 'profile',
        component: ProfileComponent,
      },
      {
        path: 'profile/:id',
        component: ProfileComponent,
      },
      {
        path: 'gift-program',
        component: GiftProgramComponent,
      },
      {
        path: 'search',
        component: UserSearchComponent,
        children: [
          { path: 'result/:id', component: ProfileViewComponent }
        ]
      },
      {
        path: 'request',
        component: RequestComponent,
      },
      { path: 'result/:id', component: ProfileViewComponent }
    ],
  },
];
export const CandidateRoutes = RouterModule.forChild(CANDIDATE_PATH);
