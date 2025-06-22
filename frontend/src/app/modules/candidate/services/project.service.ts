import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/service/auth.service';
import { IProject } from '../interfaces/IProject';

@Injectable({
  providedIn: 'root',
})
export class ProjectService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.projects;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  getProjects(userId: string) {
    if (userId === '') {
      this.userId = this.authService.getUserId();
      return this.httpClient.get<IProject[]>(
        this.url + environment.routes.getAll + '/' + this.userId
      );
    }
    return this.httpClient.get<IProject[]>(
      this.url + environment.routes.getAll + '/' + userId
    );
  }

  addProject(project: any) {
    return this.httpClient.post<IProject>(this.url, project);
  }

  deleteProject(projectId: string) {
    return this.httpClient.delete(this.url + `/${projectId}`);
  }

  updateProject(project: any) {
    return this.httpClient.put<IProject>(
      this.url + `/${project.projectGuid}`,
      project
    );
  }
}
