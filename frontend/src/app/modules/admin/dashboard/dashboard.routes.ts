import { CompanyComponent } from './company/company.component';
import { DesignationComponent } from './designation/designation.component';
import { NotFoundComponent } from './../../../shared/components/not-found/not-found.component';
import { NominationComponent } from './nomination/nomination.component';
import { RequestComponent } from './request/request.component';
import { ProfileViewComponent } from './profile-view/profile-view.component';
import { InternalProgramsDecisionComponent } from './internal-programs-decision/internal-programs-decision.component';
import { TrainingTypeComponent } from './training-type/training-type.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { InternalProgramsComponent } from './internal-programs/internal-programs.component';
import { LocationTypeComponent } from './location-type/location-type.component';
import { EmploymentTypeComponent } from './employment-type/employment-type.component';
import { PronounsComponent } from './pronouns/pronouns.component';
import { DashboardComponent } from './dashboard.component';
import { BusinessUnitsComponent } from './business-units/business-units.component';
import { Routes } from '@angular/router';
import { ProficiencyComponent } from './proficiency/proficiency.component';
import { GiftProgramComponent } from './gift-program/gift-program.component';
import { UserSearchComponent } from './user-search/user-search.component';
import { HomeComponent } from './home/home.component';
import { TrainingComponent } from './training/training.component';
import { CertificationComponent } from './certification/certification.component';
import { ProjectComponent } from './project/project.component';
import { SkillsComponent } from './skills/skills.component';
import { InternalProgramsCategoriesComponent } from './internal-programs-categories/internal-programs-categories.component';
import { DepartmentComponent } from './department/department.component';

export const dashboard_routes: Routes = [
  // { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: '',
    component: DashboardComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'business-units', component: BusinessUnitsComponent },
      { path: 'pronouns', component: PronounsComponent },
      { path: 'skills', component: SkillsComponent },
      { path: 'employment-types', component: EmploymentTypeComponent },
      { path: 'location-types', component: LocationTypeComponent },
      { path: 'training-types', component: TrainingTypeComponent },
      { path: 'internal-programs', component: InternalProgramsComponent },
      { path: 'internal-program-categories', component: InternalProgramsCategoriesComponent },
      { path: 'certifications', component: CertificationComponent },
      { path: 'departments', component: DepartmentComponent },
      { path: 'designations', component: DesignationComponent },
      { path: 'companies', component: CompanyComponent },
      { path: 'manage-users', component: ManageUsersComponent },
      {
        path: 'nominations',
        component: InternalProgramsDecisionComponent,
      },
      { path: 'proficiencies', component: ProficiencyComponent },
      { path: 'projects', component: ProjectComponent },
      { path: 'trainings', component: TrainingComponent },
      { path: 'profile/:id', component: ProfileViewComponent },
      {
        path: 'requests',
        component: RequestComponent,
      },
      {
        path: 'gift-applications',
        component: GiftProgramComponent,
      },
      {
        path: 'nominations/:id',
        component: NominationComponent,
      },
      {
        path: 'search',
        component: UserSearchComponent,
      },
      {
        path: 'not-found',
        component: NotFoundComponent,
      },
    ],
  },
];
