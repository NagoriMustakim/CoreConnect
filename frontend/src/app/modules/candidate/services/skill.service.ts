import { AuthService } from './../../auth/service/auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Skill } from '../interfaces/Skill';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SkillService {
  private url = environment.apiUrl+environment.roles.user+environment.routes.skills;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getSkills(userId: string) {
    if (userId === '') {
      this.userId = this.authService.getUserId();
      return this.httpClient.get<Skill[]>(this.url + `${environment.routes.getAll}/${this.userId}`);
    } else {
      return this.httpClient.get<Skill[]>(this.url + environment.routes.getAll+'/' + userId);
    }
  }

  addSkill(skill: any) {
    return this.httpClient.post<Skill>(this.url, skill);
  }

  deleteSkill(skillId: string) {
    return this.httpClient.delete(this.url + `/${skillId}`);
  }

  updateSkill(skill: any) {
    return this.httpClient.put<Skill>(this.url + `/${skill.skillGuid}`, skill);
  }
}
