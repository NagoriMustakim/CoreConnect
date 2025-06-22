import { NotFoundComponent } from './../../shared/components/not-found/not-found.component';
import { NominationsComponent } from './pages/nominations/nominations.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { RequestComponent } from './pages/request/request.component';
import { GiftProgramComponent } from './pages/gift-program/gift-program.component';
import { UserSearchComponent } from './pages/user-search/user-search.component';
import { ProfileViewComponent } from './pages/profile-view/profile-view.component';
import { ResumeComponent } from './components/resume/resume.component';

export const MANAGEMENT_ROUTES: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'profile', pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent },
      {
        path: 'profile/:id',
        component: ProfileComponent,
      },
      {
        path: 'gift-program',
        component: GiftProgramComponent,
      },
      {
        path: 'nominations',
        component: NominationsComponent,
      },
      {
        path: 'search',
        component: UserSearchComponent,
        children: [
          { path: 'result/:id', component: ProfileViewComponent }
        ]
      },
      { path: 'requests', component: RequestComponent },
      { path: 'result/:id', component: ProfileViewComponent },
      { path: 'resume/:businessUnitId', component: ResumeComponent },
      { path: 'not-found', component: NotFoundComponent }
    ],
  },
];
export const ManagementRoutes = RouterModule.forChild(MANAGEMENT_ROUTES);
