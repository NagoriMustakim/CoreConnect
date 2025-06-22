import { Project } from './../../../shared/interface/project';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MstProjectService {
  private url = environment.apiUrl + environment.routes.project;
  constructor(private httpClient: HttpClient) {}

  getProjects(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);
    return this.httpClient.get(this.url, { params });
  }

  addProject(project: any) {
    return this.httpClient.post<Project>(this.url, project);
  }

  updateProject(project: any) {
    return this.httpClient.put<Project>(this.url, project);
  }

  deleteProject(projectId: string) {
    return this.httpClient.delete<Project>(this.url + `/${projectId}`);
  }
}
