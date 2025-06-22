import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  private url = environment.apiUrl + environment.routes.project;
  constructor(private httpClient: HttpClient) {}

  getProjects() {
    return this.httpClient.get(this.url, { params:{
      pageNumber:0,
      pageSize:0
    } });
  }

}
