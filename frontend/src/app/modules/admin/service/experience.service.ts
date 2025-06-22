import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IExperience } from '../interfaces/IExperience';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExperienceService {
  private url =  environment.apiUrl+environment.roles.user+environment.routes.experiences;

  constructor(private http: HttpClient) { }

  getAllExperiences(userId: string) {
    return this.http.get<IExperience[]>(this.url +  environment.routes.getAll +'/' + userId);
  }
  getUserExperience(userId: string, expId: string) {
    return this.http.get<IExperience[]>(this.url + environment.roles.admin+'/' + userId + '/' + expId);
  }
  addUserExperience(userId: string, data: any) {
    console.log(data);

    return this.http.post<IExperience[]>(this.url + environment.roles.admin+'/' + userId, data);
  }
  updateExperience(userId: string, data: any) {
    return this.http.put(
      this.url + environment.roles.admin+'/' + userId + '/' + data.experienceGuid,
      data
    );
  }
  deleteExperience(userId: string, id: string) {
    return this.http.delete(this.url + environment.roles.admin+'/' + userId + '/' + id);
  }
}
