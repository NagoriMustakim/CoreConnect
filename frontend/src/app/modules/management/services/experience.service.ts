import { AuthService } from './../../auth/service/auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Experience } from '../interfaces/Experience';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExperienceService {
  private url =
    environment.apiUrl +
    environment.roles.user +
    environment.routes.experiences;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {
    this.userId = this.authService.getUserId();
  }

  getExperiences(userId: string) {
    if (userId === '') {
      return this.httpClient.get<Experience[]>(
        this.url + environment.routes.getAll + `/${this.userId}`
      );
    } else {
      return this.httpClient.get<Experience[]>(
        this.url + environment.routes.getAll + '/' + userId
      );
    }
  }

  addExperience(experience: any) {
    return this.httpClient.post<Experience>(this.url, experience);
  }

  deleteExperience(experienceId: string) {
    return this.httpClient.delete(this.url + `/${experienceId}`);
  }

  updateExperience(experience: any) {
    return this.httpClient.put<Experience>(
      this.url + `/${experience.experienceGuid}`,
      experience
    );
  }
}
