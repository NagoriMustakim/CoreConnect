import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private url = environment.apiUrl+environment.roles.user+environment.routes.skills;
  constructor(private http: HttpClient) { }

  GetUserSkills(id: string) {
    return this.http.get(`${this.url}${environment.routes.getAll}/${id}`)
  }

  AddUserSkill(data: any, id: string) {
    return this.http.post(`${this.url}${environment.roles.admin}/${id}`, data)
  }

  UpdateUserSkill(data: any, id: string) {
    return this.http.put(`${this.url}${environment.roles.admin}${environment.actions.update}/${id}/${data.skillGuid}`, data)
  }

  DeleteUserSkill(skillId: string, id: string) {
    return this.http.delete(`${this.url}${environment.roles.admin}${environment.actions.delete}/${id}/${skillId}`)
  }
}
