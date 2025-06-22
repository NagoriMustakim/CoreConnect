import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  private url = environment.apiUrl+ environment.roles.user+environment.routes.projects;
  constructor(private http: HttpClient) { }

  getProjects(userId: string) {
    return this.http.get(this.url + environment.routes.getAll+'/' + userId);
  }
  addProject(userId: string, data: any) {
    return this.http.post(this.url + environment.roles.admin+'/' + userId, data);
  }
  updateProject(userId: string, data: any) {
    return this.http.put(
      this.url + environment.roles.admin+environment.actions.update+'/' + userId + '/' + data.projectGuid,
      data
    );
  }
  deleteProject(userId: string, projectId: string) {
    return this.http.delete(
      this.url + environment.roles.admin+environment.actions.delete+'/' + userId + '/' + projectId
    );
  }
}
