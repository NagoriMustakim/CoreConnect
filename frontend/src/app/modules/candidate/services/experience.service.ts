import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/service/auth.service';
import { IExperience } from '../interfaces/IExperience';

@Injectable({
  providedIn: 'root',
})
export class ExperienceService {
  private url = environment.apiUrl + environment.roles.user + environment.routes.experiences;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getExperiences(userId: string) {
    if (userId == '') {
      this.userId = this.authService.getUserId();
      return this.httpClient.get<IExperience[]>(this.url + environment.routes.getAll + '/' + this.userId);
    } else {
      return this.httpClient.get<IExperience[]>(this.url + environment.routes.getAll + '/' + userId);
    }
  }

  addExperience(experience: any) {
    return this.httpClient.post<IExperience>(this.url, experience);
  }

  deleteExperience(experienceId: string) {
    return this.httpClient.delete(this.url + `/${experienceId}`);
  }

  updateExperience(experience: any) {
    return this.httpClient.put<IExperience>(
      this.url + `/${experience.experienceGuid}`,
      experience
    );
  }
}
